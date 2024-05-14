using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostEssencesController : MonoBehaviour
{
    public int currency;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>() != null)
        {
            PlayerManager.instance.essenceAmount += currency;
            Destroy(this.gameObject);
        }
        
    }
}
