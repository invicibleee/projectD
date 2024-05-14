using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private GameObject alternativeCamera;
    private bool isMainCameraActive;

    private void TriggerAction()
    {
        isMainCameraActive = mainCamera.gameObject.activeInHierarchy;

        if (isMainCameraActive) {

            alternativeCamera.SetActive(true);
            mainCamera.gameObject.SetActive(false);
        }
        else
        {
            mainCamera.gameObject.SetActive(true);
            alternativeCamera.SetActive(false);
        }
      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerAction();
        }
    }
}
