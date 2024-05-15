using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExit : MonoBehaviour
{

    private BossGUI boss;
    [SerializeField] private GameObject trigger;
    [SerializeField] private bool status;
    [SerializeField] private GameObject bossHud;
    void Start()
    {
        boss = bossHud.GetComponent<BossGUI>();
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
