using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [SerializeField] private bool isKnifeRight;
    [SerializeField] private float moveSpeed;

    void Start()
    {
        if (isKnifeRight) moveSpeed *= -1;
        
        Invoke("Deactivate", 3f);
    }
    
    void Deactivate()
    {
        gameObject.SetActive(false);
    }

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

}
