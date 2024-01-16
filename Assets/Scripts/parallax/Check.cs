using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{

    public float speed = 15f;
    public float cameraFollowSpeed = 15f;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput,verticalInput) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        CameraFollowPlayer();
    }

    void CameraFollowPlayer()
    {


        // ѕолучаем текущую позицию игрока
        Vector3 playerPosition = transform.position;

        // ”станавливаем новую позицию камеры без использовани€ Lerp дл€ более пр€мого следовани€
        Camera.main.transform.position = new Vector2(playerPosition.x, playerPosition.y);
    }
}
