using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    public CinemachineVirtualCamera forestFollowCamera;
    public GameObject alternativeCamera;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleCameras();
        }
    }


    private void ToggleCameras()
    {
        forestFollowCamera.enabled = !forestFollowCamera.enabled;
        alternativeCamera.SetActive(!alternativeCamera.activeSelf);

        if (forestFollowCamera.enabled)
        {
            Debug.Log("Forest follow camera enabled");
        }
        else
        {
            Debug.Log("Alternative camera enabled");
        }
    }
}
