using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float boundLeft, boundRight;
    [SerializeField] private float moveSpeed;

    void Awake()
    {
        if (Random.Range(0, 2) > 0) moveSpeed *= -1f;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x += moveSpeed * Time.deltaTime;

        if (currentPosition.x < boundLeft || currentPosition.x > boundRight) moveSpeed *= -1f;

        transform.position = currentPosition;
    }
}
