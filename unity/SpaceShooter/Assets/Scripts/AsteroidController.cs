using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    void Start() => Invoke("Deactivate", 15f);

    void Update()
    {
        Move();
    }

    void Deactivate() => gameObject.SetActive(false);

    void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x -= moveSpeed * Time.deltaTime;
        
        transform.position = currentPosition;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerBullet"))
        {
            GetComponent<Animator>().Play("AsteroidTypeOneDestroy");
            collider.gameObject.SetActive(false);
        }
    }
}
