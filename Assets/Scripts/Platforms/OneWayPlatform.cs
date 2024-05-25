using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime = 0.4f;  // Time to wait before allowing drop again
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

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isDropping)
        {
            effector.rotationalOffset = 180f;
            timer = waitTime;
            isDropping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isDropping)
        {
            effector.rotationalOffset = 0f;
        }
    }
}
