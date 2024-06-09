using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LathernActivation : MonoBehaviour
{
    [SerializeField] private GameObject lathern;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Player") && InventoryPanelScript.instance.items[4].isOwned)
          {
             lathern.SetActive(true);
          }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && InventoryPanelScript.instance.items[4].isOwned)
        {
            lathern.SetActive(false);
        }
    }
}
