using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (InventoryPanelScript.instance.items[3].isOwned)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryPanelScript.instance.items[3].isOwned && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
