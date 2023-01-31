using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed;
    [SerializeField] private float minSpeed, maxSpeed;

    void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        Invoke("Deactivate", 3f);
    }

    void Deactivate() => gameObject.SetActive(false);

    void FixedUpdate()
    {
        Vector2 forward = new Vector2(transform.right.x, transform.right.y);
        GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + forward * Time.fixedDeltaTime * speed);
    }
}
