using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Player player; 
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected with ");
        // ���������, ����� �� ������ ��� "Player"
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            Debug.Log("PlayerHealth component found");
            // ������������� �������� ������ � 0
            player.GetStats().TakeDamage(10000);
        }
    }
}
