using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject explosion;
    [HideInInspector] public EnemySpawner enemySpawner;
    private Transform followTarget;

    void Awake() => followTarget = GameObject.FindWithTag("Player").transform;

    void FixedUpdate()
    {
        Vector2 pointToTarget = (Vector2)transform.position - (Vector2)followTarget.position;
        pointToTarget.Normalize();

        float value = Vector3.Cross(pointToTarget, transform.up).z;

        GetComponent<Rigidbody2D>().velocity = transform.up * moveSpeed;
        GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed * value;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Mine"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            enemySpawner.ActivateSpawning();
        }    
    }
}
