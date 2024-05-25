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
    private bool isMainCameraActive;
    private string saveKey = "camera";

    private void Awake()
    {
        Load();
        mainCamera.gameObject.SetActive(isMainCameraActive);
    }
    private void TriggerAction()
    {
        if (isMainCameraActive) {

            alternativeCamera.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            isMainCameraActive = false;
        }
        else
        {
            mainCamera.gameObject.SetActive(true);
            alternativeCamera.SetActive(false);
            isMainCameraActive = true;
        }
      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerAction();
        }
    }
    public void Save1()
    {
        SaveManager.Save(saveKey, GetData1());

    }
    public void Save2()
    {
        SaveManager.Save(saveKey, GetData2());

    }
    public void Save3()
    {
        SaveManager.Save(saveKey, GetData3());

    }
    public void Save4()
    {
        SaveManager.Save(saveKey, GetData4());

    }
    private void OnApplicationQuit()
    {
        if (index == 1)
        {
            Save1();
           
        }
        else if (index == 2)
        {
            Save2();
           
        }
        else if (index == 3)
        {
            Save3();
        }
        else if (index == 4)
        {
            Save4();
        }
    }
    private void Load()
    {
        var data = SaveManager.Load<SaveData.CameraActivity>(saveKey);
        if(index == 1)
        {
            isMainCameraActive = data._isMainCameraActive1;
            Debug.Log("camera 1 status" + isMainCameraActive);
        } else if (index == 2)
        {
            isMainCameraActive = data._isMainCameraActive2;
            Debug.Log("camera 2 status" + isMainCameraActive);
        }
        else if (index == 3)
        {
            isMainCameraActive = data._isMainCameraActive3;
        }
        else if (index == 4)
        {
            isMainCameraActive = data._isMainCameraActive4;
        }

    }

    private SaveData.CameraActivity GetData1()
    {
        var data = new SaveData.CameraActivity()
        {
            _isMainCameraActive1 = isMainCameraActive,
            _isMainCameraActive2 = !isMainCameraActive,
        };
        return data;
    }
    private SaveData.CameraActivity GetData2()
    {
        var data = new SaveData.CameraActivity()
        {
            _isMainCameraActive1 = !isMainCameraActive,
            _isMainCameraActive2 = isMainCameraActive,
        };
        return data;
    }
    private SaveData.CameraActivity GetData3()
    {
        var data = new SaveData.CameraActivity()
        {
            _isMainCameraActive3 = isMainCameraActive,
            _isMainCameraActive4 = !isMainCameraActive,
        };
        return data;
    }
    private SaveData.CameraActivity GetData4()
    {
        var data = new SaveData.CameraActivity()
        {
            _isMainCameraActive3 = !isMainCameraActive,
            _isMainCameraActive4 = isMainCameraActive,
        };
        return data;
    }
}
