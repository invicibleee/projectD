using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TalantCollisison : MonoBehaviour
{
    private TalantsPanelScript talantsPanelScript;
    [SerializeField] private int talantIndex;
    [SerializeField] private Text message;
    [SerializeField] private string text;

    private bool isPlayerNearby = false;

    private void Start()
    {
        talantsPanelScript = TalantsPanelScript.instance;
    }
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            talantsPanelScript.SetTalantOwned(talantIndex);
            Destroy(gameObject);
            message.text = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            message.text = text;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            message.text = "";
        }
    }

}
