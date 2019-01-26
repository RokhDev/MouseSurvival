using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mice : MonoBehaviour
{
    Transform playerTransform;
    Vector3 movementVector;
    int x;
    int y;
    const int playerSpeed = 5;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        movementVector = new Vector3();
        x = 0;
        y = 0;
    }

    void Update()
    {
        PlayerMove();
    }
    public void PlayerMove()
    {
        x = 0;
        y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            x = 0;
            y = playerSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            x = 0;
            y = -playerSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x = -playerSpeed;
            y = 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = playerSpeed;
            y = 0;
        }
        movementVector.x = x;
        movementVector.y = y;
        playerTransform.position += movementVector * Time.deltaTime;
    }
}
