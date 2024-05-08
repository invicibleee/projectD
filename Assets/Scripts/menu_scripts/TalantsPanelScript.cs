using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Talant
{
    public Image image;
    public bool isOwned;
    public string description;
    public string name;
}


public class TalantsPanelScript : MonoBehaviour
{
    public Talant[] talants;
    public Text descriptionText;
    public Text nameText;
    private int selectedTalantIndex = -1;
    public static TalantsPanelScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        descriptionText.text = "select talant";
        nameText.text = "";
        UpdateTalantImages();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (selectedTalantIndex != -1) {
                talants[selectedTalantIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            descriptionText.text = "select talant";
            nameText.text = "";
            UpdateTalantImages();
        }
    }


    public void OnTalantImageClick(int talantIndex)
    {
        UpdateDescriptionText(talantIndex);

        if (selectedTalantIndex != -1)
        {
            talants[selectedTalantIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (talants[talantIndex].isOwned) {
            talants[talantIndex].image.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            selectedTalantIndex = talantIndex;
            nameText.text = talants[talantIndex].name;
        }

    }

    void UpdateDescriptionText(int talantIndex)
    {
        if (talantIndex >= 0 && talantIndex < talants.Length && talants[talantIndex].isOwned)
        {
            Debug.Log(talants[talantIndex].description);
            descriptionText.text = talants[talantIndex].description;
        }
        else
        {
            //  Debug.LogError("Invalid description index: " + talantIndex);
        }
    }
    void UpdateTalantImages()
    {
        for (int i = 0; i < talants.Length; i++)
        {
            // Set image color based on whether the charm is purchased or not
            talants[i].image.color = talants[i].isOwned ? Color.white : Color.gray;
        }
    }

    public void SetTalantOwned(int talantIndex)
    {
        talants[talantIndex].isOwned = true;
        Debug.Log("You found Charm Index: " + talantIndex);
        UpdateTalantImages();

    }
}
