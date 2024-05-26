using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraChangeTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private GameObject alternativeCamera;
    [SerializeField] private int index;
    [SerializeField] private CameraChangeTrigger otherCameraTrigger;
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
        if (index == 1)
        {
            data._isMainCameraActive1 = isMainCameraActive;
        }
        else if (index == 2)
        {
            data._isMainCameraActive2 = isMainCameraActive;
        }
        SaveManager.Save(saveKey, data);
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.CameraActivity>(saveKey);
        if (data != null)
        {
            if (index == 1)
            {
                isMainCameraActive = data._isMainCameraActive1;
                Debug.Log("Camera 1 status: " + isMainCameraActive);
            }
            else if (index == 2)
            {
                isMainCameraActive = data._isMainCameraActive2;
                Debug.Log("Camera 2 status: " + isMainCameraActive);
            }
        }
    }
}
