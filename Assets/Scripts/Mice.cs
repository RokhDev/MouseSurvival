using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mice : MonoBehaviour
{
    Transform playerTransform;
    Vector3 movementVector;
    int x;
    int y;
    const int playerSpeed = 3;
    int foodCount;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        movementVector = new Vector3();
        x = 0;
        y = 0;
        foodCount = 0;
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

    public int GetFoodCount()
    {
        return foodCount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cheese")
        {
            foodCount += 1;
            other.enabled = false;
            other.gameObject.SetActive(false);
            Debug.Log("Food Count = " + foodCount);
        }

        if (other.tag == "Home")
        {
            if (foodCount != 0)
            {
                other.gameObject.GetComponent<Home>().SumScore(foodCount);
                foodCount = 0;
                Debug.Log("Food Count = " + foodCount);
                Debug.Log("Total Score = " + other.gameObject.GetComponent<Home>().GetScore());
            }
        }
    }
}
