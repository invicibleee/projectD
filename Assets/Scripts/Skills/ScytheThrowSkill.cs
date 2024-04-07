using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheThrowSkill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject scythePrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float scytheGravity;


    private Vector2 finalDirection;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotsPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject []dots;

    protected override void Start()
    {
        base.Start();

        GenerateDots();
    }
    protected override void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse1))
            finalDirection = new Vector2(AimDirection().normalized.x * launchForce.x , AimDirection().normalized.y * launchForce.y);

        if(Input.GetKey(KeyCode.Mouse1))
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

        newScytheScript.SetupScythe(finalDirection, scytheGravity);

        DotsActive(false);
    }

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
}
