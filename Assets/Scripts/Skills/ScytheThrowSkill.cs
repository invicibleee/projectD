using System;
using UnityEngine;


public enum ScytheType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class ScytheThrowSkill : Skill
{
    public ScytheType scytheType = ScytheType.Regular;

    [Header("Bounce info")]
    [SerializeField] private int BounceAmount;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;

    [Header("Pierce info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    [Header("Spin info")]
    [SerializeField] private float hitCooldown = 0.2f;
    [SerializeField] private float maxTravelDistance = 5;
    [SerializeField] private float spinDuration = 2;
    [SerializeField] private float spinGravity = 1;


    [Header("Skill info")]
    [SerializeField] private GameObject scythePrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float scytheGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;


    private Vector2 finalDirection;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotsPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        GenerateDots();

        SetupGravity();
    }

    private void SetupGravity()
    {
        if (scytheType == ScytheType.Bounce)
            scytheGravity = bounceGravity;
        else if(scytheType == ScytheType.Pierce)
            scytheGravity = pierceGravity;
        else if(scytheType == ScytheType.Spin)
            scytheGravity = spinGravity;
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDirection = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }

    public void CreateScythe()
    {
        GameObject newScythe = Instantiate(scythePrefab, player.transform.position, transform.rotation);
        ScytheSkillController newScytheScript = newScythe.GetComponent<ScytheSkillController>();

        if (scytheType == ScytheType.Bounce)
            newScytheScript.SetupBounce(true, BounceAmount, bounceSpeed);
        else if (scytheType == ScytheType.Pierce)
            newScytheScript.SetupPierce(pierceAmount);
        else if (scytheType == ScytheType.Spin)
            newScytheScript.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);

        newScytheScript.SetupScythe(finalDirection, scytheGravity, player, freezeTimeDuration, returnSpeed);

        player.AssingNewScythe(newScythe);

        DotsActive(false);
    }
    #region Aim region
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }
    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotsPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
          AimDirection().normalized.x * launchForce.x,
          AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * scytheGravity) * (t * t);

        return position;

    }
    #endregion
}
