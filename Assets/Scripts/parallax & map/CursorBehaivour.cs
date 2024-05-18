using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaivour : MonoBehaviour
{

    [SerializeField] private Transform targetObject;
    [SerializeField] private float speed = 10.0f;

    void Start()
    {
        transform.position = targetObject.position;
    }

    void Update()
    {
        Vector2 targetPosition = targetObject.position;
        Vector3 direction = (targetPosition - (Vector2)transform.position).normalized;

        float distanceToMove = speed * Time.deltaTime;
        Vector3 newPosition = transform.position + direction * distanceToMove;

        newPosition.z =100;
        transform.position = newPosition;

    }
}
