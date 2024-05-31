using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime = 0.4f;  // Время ожидания перед разрешением падения
    private float timer = 0f;
    private bool isDropping = false;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (isDropping)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                effector.rotationalOffset = 0f;
                isDropping = false;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !isDropping)
            {
                effector.rotationalOffset = 180f;
                timer = waitTime;
                isDropping = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            effector.rotationalOffset = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, сталкивается ли с платформой игрок
        if (collision.collider.CompareTag("Player"))
        {
            // Проверяем, находится ли игрок выше платформы
            if (collision.contacts[0].point.y > transform.position.y)
            {
                // Если игрок находится выше платформы, разрешаем ему падение
                effector.rotationalOffset = 0f;
            }
        }
        // Проверяем, сталкивается ли с платформой враг
        else if (collision.collider.CompareTag("Enemy"))
        {
            // Если это враг, сбрасываем эффект платформы, чтобы он не упал
            effector.rotationalOffset = 0f;
        }
    }
}
