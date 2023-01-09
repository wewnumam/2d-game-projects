using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x += moveSpeed * Time.deltaTime;

        transform.position = currentPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SideBound"))
        {
            moveSpeed *= -1f;

            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1f;

            transform.localScale = currentScale;
        }
    }
}
