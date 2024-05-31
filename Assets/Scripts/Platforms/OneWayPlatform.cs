using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime = 0.4f;  // ����� �������� ����� ����������� �������
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
        // ���������, ������������ �� � ���������� �����
        if (collision.collider.CompareTag("Player"))
        {
            // ���������, ��������� �� ����� ���� ���������
            if (collision.contacts[0].point.y > transform.position.y)
            {
                // ���� ����� ��������� ���� ���������, ��������� ��� �������
                effector.rotationalOffset = 0f;
            }
        }
        // ���������, ������������ �� � ���������� ����
        else if (collision.collider.CompareTag("Enemy"))
        {
            // ���� ��� ����, ���������� ������ ���������, ����� �� �� ����
            effector.rotationalOffset = 0f;
        }
    }
}
