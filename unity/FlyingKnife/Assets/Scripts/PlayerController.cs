using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    const float MAX_POSITION_ON_Y_AXIS = 4.4f;

    void Update()
    {
        Jump();
        KeepPlayerOnBounds();
    }

    void Jump()
    {
        if (GameManager.Instance.isGameOver) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
        }
    }

    void KeepPlayerOnBounds()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.y > MAX_POSITION_ON_Y_AXIS) currentPosition.y = MAX_POSITION_ON_Y_AXIS;

        transform.position = currentPosition; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.KNIFE_TAG))
        {
            GameManager.Instance.RestartGame();
        }    
    }
}
