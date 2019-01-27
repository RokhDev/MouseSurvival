﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mice : MonoBehaviour
{
    public LayerMask wallLayer;
    [Range(0.0f,1.0f)]
    public float transparentWallAlpha;

    private float originalWallAlpha = 0;
    private Collider2D lastWallHit;

    Transform playerTransform;
    Vector3 movementVector;
    int x;
    int y;
    const int playerSpeed = 3;
    int foodCount;
    const float diagonalCos = 0.7071067f;

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
        WallTransparency();
    }

    public void PlayerMove()
    {
        x = 0;
        y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            y = playerSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y = -playerSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x = -playerSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = playerSpeed;
        }
        movementVector.x = x;
        movementVector.y = y;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            playerTransform.position += movementVector * Time.deltaTime * diagonalCos;
        }
        else
        {
            playerTransform.position += movementVector * Time.deltaTime;
        }
    }

    public int GetFoodCount()
    {
        return foodCount;
    }

    public void WallTransparency()
    {
        Collider2D[] hit = Physics2D.OverlapPointAll(new Vector2(transform.position.x, transform.position.y), wallLayer);
        if (hit.Length > 0)
        {
            if (lastWallHit)
            {
                SpriteRenderer lsr = lastWallHit.gameObject.GetComponent<Renderer>() as SpriteRenderer;
                lsr.color = new Color(lsr.color.r, lsr.color.g, lsr.color.b, originalWallAlpha);
            }
            lastWallHit = hit[0];
            SpriteRenderer sr = hit[0].gameObject.GetComponent<Renderer>() as SpriteRenderer;
            if(originalWallAlpha == 0)
                originalWallAlpha = sr.color.a;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, transparentWallAlpha);
        }
        else
        {
            if (lastWallHit)
            {
                SpriteRenderer sr = lastWallHit.gameObject.GetComponent<Renderer>() as SpriteRenderer;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, originalWallAlpha);
                lastWallHit = null;
                originalWallAlpha = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cheese")
        {
            foodCount += 1;
            other.enabled = false;
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Home")
        {
            if (foodCount != 0)
            {
                other.gameObject.GetComponent<Home>().SumScore(foodCount);
                foodCount = 0;
            }
        }
    }
}
