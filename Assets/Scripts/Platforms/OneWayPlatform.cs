using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime = 0.4f;  // Time to wait before allowing drop again
    private float timer = 0f;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.S) ||  Input.GetKeyDown(KeyCode.DownArrow)) && timer <= 0)
        {
            effector.rotationalOffset = 180f;
            timer = waitTime;
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            effector.rotationalOffset = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            effector.rotationalOffset = 0f;
        }
    }
}