using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject map;
    private string saveKey = "tutorialSave";
    private bool status;
    // Start is called before the first frame update

    private void Awake()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && tutorial.activeSelf)
        {
            CloseTutorial();
        }
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
        map.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !status)
        {
            status = true;
            tutorial.SetActive(true);
            map.SetActive(false);
            Save();
        }
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }
    private void Load()
    {
        var data = SaveManager.Load<SaveData.TutorialSave>(saveKey);
        status = data._tutorailShowed;
    }
    private SaveData.TutorialSave GetData()
    {
        var data = new SaveData.TutorialSave()
        {
          _tutorailShowed = status,
        };
        return data;
    }
}
