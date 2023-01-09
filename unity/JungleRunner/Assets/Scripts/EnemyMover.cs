using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float cameraY;

    void Awake()
    {
        cameraY = Camera.main.gameObject.transform.position.y;
    }
    
    void Update()
    {
        Move();
        Deactivate();
    }

    void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y -= moveSpeed * Time.deltaTime;

        transform.position = currentPosition;
    }

    void Deactivate()
    {
        const float CAMERA_SPACE_BEFORE_DEACTIVATE_FLAG = 15f;
        if (transform.position.y < cameraY - CAMERA_SPACE_BEFORE_DEACTIVATE_FLAG)
        {
            gameObject.SetActive(false);
        }
    }
}
