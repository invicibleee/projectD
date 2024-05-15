using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaivour : MonoBehaviour
{

    [SerializeField] private Transform targetObject;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private GameObject mainCamera; 
    private bool isObjectActive = false; 

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        Vector2 targetPosition = targetObject.position;
        Vector3 direction = (targetPosition - (Vector2)transform.position).normalized;

        float distanceToMove = speed * Time.deltaTime;
        Vector3 newPosition = transform.position + direction * distanceToMove;

        newPosition.z =100;
        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.M))
        {
            isObjectActive = !isObjectActive;

            mainCamera.SetActive(isObjectActive);
        }
    }
}
