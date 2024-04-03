using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExit : MonoBehaviour
{

    BossHud boss;
    public GameObject trigger;
    public bool status;
    public GameObject bossHud;
    void Start()
    {
        boss = bossHud.GetComponent<BossHud>();
        if (boss == null)
        {
            Debug.LogError("BossHud not found!");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (boss.GetStatus(status))
            {
                trigger.SetActive(true);
            }
        }
    }

}
