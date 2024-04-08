using UnityEngine;

public class ProjectileLazer : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private float startTime;
    private Transform projectileRotation;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float maxAccelerationTime = 2f; // Час для досягнення максимального прискорення
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private float destroyDelay = 2f; // Затримка перед знищенням об'єкта

    public Transform startPoint; // Початкова точка лазера
    public float maxDistance = 10f; // Максимальна дистанція лазера
    public LayerMask layerMask; // Шари для виявлення колайдерів
    public LineRenderer lineRenderer; // Компонент для відображення лінії лазера

    private EnemyEye enemyEye;

    private void Start()
    {
        // Початковий поворот об'єкта
        transform.rotation = Quaternion.Euler(0, 0, -45);
        startTime = Time.time;
        enemyEye = GetComponent<EnemyEye>();
    }

    private void Update()
    {
        if (startPoint != null && lineRenderer != null)
        {
            // Виконуємо виявлення першого зіткнення з колайдером на шарах "Player" або "Ground"
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, Quaternion.Euler(0, projectileRotation.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * Vector2.right, maxDistance, layerMask);
            Vector2 endPosition; // Позиція кінця лазера

            if (hit.collider != null)
            {
                // Якщо зіткнення відбулося, встановлюємо кінцеву точку лазера на місці зіткнення
                endPosition = hit.point;

                // Перевіряємо, чи зіткнення відбулося з об'єктом на заданому шарі
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    // Викликаємо метод Damage на об'єкті, з яким зіткнувся лазер
                    hit.collider.gameObject.SendMessage("Damage", attackDetails);
                }
            }
            else
            {
                // Встановлюємо кінцеву точку лазера на максимальній дистанції, слідкуючи за зміною позиції початкової точки
                endPosition = startPoint.position + Quaternion.Euler(0, projectileRotation.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * Vector2.right * maxDistance;

            }

            // Переміщаємо кінцеву точку лазера в напрямку його напрямку
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPosition);
        }

        // Визначаємо поточний час, що пройшов з моменту початку вогневої дії
        float timeElapsed = Time.time - startTime;

        // Розраховуємо поточну швидкість обертання залежно від часу
        float currentRotationSpeed = Mathf.Lerp(0f, rotationSpeed * 10f, Mathf.Clamp01(timeElapsed / maxAccelerationTime));

        // Обертаємо об'єкт
        transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);

        // Перевіряємо, чи час знищення об'єкта вже настав
        if (Time.time >= startTime + destroyDelay)
        {
            // Знищуємо об'єкт
            Debug.Log("Лазер знищено");
            Destroy(gameObject);
        }
    }

    public void FireProjectile(float damage, Transform transform)
    {
        attackDetails.damageAmount = damage;
        projectileRotation = transform;
    }
}
