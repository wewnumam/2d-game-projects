using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPosition;

    void Start()
    {
        transform.position = targetPosition;
    }

    void Update()
    {
        const float SMOOTH_MOVE = 1f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, SMOOTH_MOVE * Time.deltaTime);
    }
}
