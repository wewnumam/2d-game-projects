using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalMoveSpeed;

    void FixedUpdate()
    {
        MoveRight();
        MoveLeft();
    }

    void MoveRight()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMoveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void MoveLeft()
    {
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalMoveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void MoveOnSpeedPlatform(float horizontalMoveSpeed)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMoveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }
}
