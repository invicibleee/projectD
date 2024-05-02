using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMapIcon : MonoBehaviour
{

    public Transform targetObject; 
    public float speed = 10.0f;
    public GameObject mainCamera; 
    private bool isObjectActive = false; 

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        // �������� ������� ��������� ��'����
        Vector2 targetPosition = targetObject.position;

        // ����������� ������ �������� �� ���
        Vector3 direction = (targetPosition - (Vector2)transform.position).normalized;

        // ����������� �������, ��� �� ������ ��'��� �� ����� ����
        float distanceToMove = speed * Time.deltaTime;

        // �������� ������� �������� Z
        float currentZ = transform.position.z;

        // ����������� ���� ���������, ������� �� ��������� ��������� �������, ��� �� ������ ��'���
        Vector3 newPosition = transform.position + direction * distanceToMove;

        // ������ �������� Z �� 0
        newPosition.z =100 ;

        // ������������ ���� ��������� ��'����
        transform.position = newPosition;




        if (Input.GetKeyDown(KeyCode.M))
        {
            isObjectActive = !isObjectActive;

            mainCamera.SetActive(isObjectActive);
        }
    }
}
