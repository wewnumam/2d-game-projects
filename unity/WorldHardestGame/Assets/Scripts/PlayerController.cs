using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    
    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;

        if (Input.GetAxisRaw("Horizontal") > 0)
            currentPosition.x += moveSpeed * Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") < 0)
            currentPosition.x -= moveSpeed * Time.deltaTime;
        if (Input.GetAxisRaw("Vertical") > 0)
            currentPosition.y += moveSpeed * Time.deltaTime;
        if (Input.GetAxisRaw("Vertical") < 0)
            currentPosition.y -= moveSpeed * Time.deltaTime;
        
        transform.position = currentPosition;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.PlayerDied();
        }

        if (collider.gameObject.CompareTag("Goal"))
        {
            GameManager.Instance.PlayerReachedGoal();
        }    
    }
}
