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
        // Отримуємо поточне положення об'єкта
        Vector2 targetPosition = targetObject.position;

        // Розраховуємо вектор напрямку до цілі
        Vector3 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Розраховуємо відстань, яку має пройти об'єкт за кожен кадр
        float distanceToMove = speed * Time.deltaTime;

        // Зберігаємо поточне значення Z
        float currentZ = transform.position.z;

        // Розраховуємо нове положення, додавши до поточного положення відстань, яку має пройти об'єкт
        Vector3 newPosition = transform.position + direction * distanceToMove;

        // Задаємо значення Z як 0
        newPosition.z =100 ;

        // Встановлюємо нове положення об'єкта
        transform.position = newPosition;




        if (Input.GetKeyDown(KeyCode.M))
        {
            isObjectActive = !isObjectActive;

            mainCamera.SetActive(isObjectActive);
        }
    }
}
