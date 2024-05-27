using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private bool isOpen;
    [SerializeField] private int index;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Animator doorAnimator;
    private string saveKey = "doorSave";

    private void Start()
    {
    
       // isOpen = false;
        doorAnimator = door.GetComponent<Animator>();
        Load();
    }
    private IEnumerator UpdateAnimation()
    {
        yield return new WaitForSeconds(1f);
        doorAnimator.SetBool("isFalling", false);
    }

    private void Update()
    {
        if (isOpen)
        {
            doorAnimator.SetBool("isStaying", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            Debug.Log("Open");
            OpenDoor();
            StartCoroutine(UpdateAnimation());
            isOpen = true;
            Save();
        }
    }

    private void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isFalling", true);
        }
    }

    public void SetOpen()
    {
        isOpen = true;
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.DoorSave>(saveKey);
       
            switch (index)
            {
                case 1:
                    isOpen = data._doorOpenOne;
                    break;
                case 2:
                    isOpen = data._doorOpenTwo;
                    break;
                case 3:
                    isOpen = data._doorOpenThree;
                    break;
            }
    }

    private SaveData.DoorSave GetData()
    {
        var data = SaveManager.Load<SaveData.DoorSave>(saveKey);
        if (data == null) data = new SaveData.DoorSave();

        switch (index)
        {
            case 1:
                data._doorOpenOne = isOpen;
                break;
            case 2:
                data._doorOpenTwo = isOpen;
                break;
            case 3:
                data._doorOpenThree = isOpen;
                break;
        }

        return data;
    }
}
