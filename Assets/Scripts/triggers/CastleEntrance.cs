using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntrance : MonoBehaviour
{
    public GameObject door;
    public bool isOpen; 
    public float rotationSpeed = 90f;
    public Animator doorAnimator;

    private void Start()
    {
        isOpen = false;
        doorAnimator = door.GetComponent<Animator>();
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
            StartCoroutine(UpdateAnimation());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            Debug.Log("Open");
            OpenDoor();
            isOpen = true;
        }
    }

    private void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isFalling", true);
        }
        else
        {
            Debug.LogError("Door animator is null!");
        }
    }

}
