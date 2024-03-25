using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronofistSkillController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    public float maxSize;
    public float growSpeed;
    public bool canGrow;

    private bool canAttack;
    public int amountOfAttacks = 4;
    public float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;

    public List<Transform> targets = new List<Transform>();

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
            canAttack = true;

        if(cloneAttackTimer < 0 )
        {
            cloneAttackTimer = cloneAttackCooldown;
            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;

            if (Random.Range(0, 100) > 50)
                xOffset = 2;
            else
            {
                xOffset = -2;
            }
            SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));
            amountOfAttacks--;

            if(amountOfAttacks <= 0 )
            {
                canAttack = false;
            }
        }


        if(canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {

        if(keyCodeList.Count <= 0)
        {
            Debug.Log("Not enough hot keys");
            return;
        }
        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        ChronofistHotkeyController newHotKeyScript = newHotKey.GetComponent<ChronofistHotkeyController>();

        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
