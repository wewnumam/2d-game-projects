using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    [SerializeField] private GameObject shuriken;
    [SerializeField] private float moveSpeed;
    
    private float cameraY;
    private bool hasAttack;

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

        if (!IsReachYAxisToAttack()) return;

        if (!hasAttack)
        {
            hasAttack = true;
            Instantiate(shuriken, transform.position, Quaternion.identity);
        }
    }

    void Deactivate()
    {
        const float CAMERA_SPACE_BEFORE_DEACTIVATE_NINJA = 15f;
        if (transform.position.y < cameraY - CAMERA_SPACE_BEFORE_DEACTIVATE_NINJA)
        {
            gameObject.SetActive(false);
        }
    }

    bool IsReachYAxisToAttack()
    {
        const float POSITION_ON_Y_AXIS_TO_ATTACK = 0;
        return transform.position.y < POSITION_ON_Y_AXIS_TO_ATTACK;
    }
}
