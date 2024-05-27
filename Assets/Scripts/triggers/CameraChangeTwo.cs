using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeTwo : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private GameObject alternativeCamera;
    [SerializeField] private int index;
    [SerializeField] private CameraChangeTwo otherCameraTrigger;
    public bool isMainCameraActive;
    private string saveKey = "camera";

    private void Awake()
    {
        Load();
        mainCamera.gameObject.SetActive(isMainCameraActive);
        alternativeCamera.gameObject.SetActive(!isMainCameraActive);
    }

    private void TriggerAction()
    {
        isMainCameraActive = !isMainCameraActive;
        mainCamera.gameObject.SetActive(isMainCameraActive);
        alternativeCamera.gameObject.SetActive(!isMainCameraActive);

        if (otherCameraTrigger != null)
        {
            otherCameraTrigger.SetCameraState(!isMainCameraActive);
        }

        Save();
    }

    public void SetCameraState(bool state)
    {
        isMainCameraActive = state;
        mainCamera.gameObject.SetActive(isMainCameraActive);
        alternativeCamera.gameObject.SetActive(!isMainCameraActive);
        Save();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerAction();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        var data = SaveManager.Load<SaveData.CameraActivity>(saveKey) ?? new SaveData.CameraActivity();
        if (index == 3)
        {
            data._isMainCameraActive3 = isMainCameraActive;
        }
        else if (index == 4)
        {
            data._isMainCameraActive4 = isMainCameraActive;
        }
        SaveManager.Save(saveKey, data);
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.CameraActivity>(saveKey);
        if (data != null)
        {
            if (index == 3)
            {
                isMainCameraActive = data._isMainCameraActive3;
                Debug.Log("Camera 3 status: " + isMainCameraActive);
            }
            else if (index == 4)
            {
                isMainCameraActive = data._isMainCameraActive4;
                Debug.Log("Camera 4 status: " + isMainCameraActive);
            }
        }
    }
}
