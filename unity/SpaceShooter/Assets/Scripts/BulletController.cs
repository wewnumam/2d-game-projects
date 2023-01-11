using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isEnemyBullet;

    void Start()
    {
        if (isEnemyBullet) moveSpeed *= -1f;
        Invoke("Deactivate", 4f);
    }

    void Update() => Move();

    void Deactivate() => gameObject.SetActive(false);

    void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x += moveSpeed * Time.deltaTime;
        
        transform.position = currentPosition;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerBullet") || collider.gameObject.CompareTag("EnemyBullet"))
        {
            collider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
