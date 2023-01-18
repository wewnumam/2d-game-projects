using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceBetweenNewPosition;
    private float newPositionOnYAxis;
    private bool canMove;

    void OnEnable()
    {
        PlayerController.move += Move;
    }

    void OnDisable()
    {
        PlayerController.move -= Move;        
    }

    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if (canMove)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.y += moveSpeed * Time.deltaTime;
            transform.position = currentPosition;
        }
        if (transform.position.y >= newPositionOnYAxis) canMove = false;
    }

    void Move()
    {
        newPositionOnYAxis = transform.position.y + distanceBetweenNewPosition;
        canMove = true;
    }
}
