using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private bool isMoveLeft;

    void Update()
    {
        Vector3 currentPosition = transform.position;
        if (isMoveLeft)
            currentPosition.x -= moveSpeed * Time.deltaTime;
        else
            currentPosition.x += moveSpeed * Time.deltaTime;
        
        transform.position = currentPosition;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bounce"))
        {
            isMoveLeft = !isMoveLeft;
        }    
    }
}
