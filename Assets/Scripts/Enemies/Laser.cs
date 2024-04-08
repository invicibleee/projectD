using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform startPoint; // Початкова точка лазера
    public float maxDistance = 10f; // Максимальна дистанція лазера
    public LayerMask layerMask; // Шари для виявлення колайдерів
    public LineRenderer lineRenderer; // Компонент для відображення лінії лазера
    public float rotationSpeed = 1f; // Швидкість обертання лазера

    void Update()
    {
        if (startPoint != null && lineRenderer != null)
        {
            // Виконуємо виявлення першого зіткнення з колайдером на шарах "Player" або "Ground"
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right, maxDistance, layerMask);

            Vector2 endPosition; // Позиція кінця лазера

            if (hit.collider != null)
            {
                // Якщо зіткнення відбулося, встановлюємо кінцеву точку лазера на місці зіткнення
                endPosition = hit.point;
            }
            else
            {
                // Встановлюємо кінцеву точку лазера на максимальній дистанції, слідкуючи за зміною позиції початкової точки
                endPosition = startPoint.position + startPoint.right * maxDistance;
            }

            // Переміщаємо кінцеву точку лазера в напрямку його напрямку
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPosition);
        }
    }



}
