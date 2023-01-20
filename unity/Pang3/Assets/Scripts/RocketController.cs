using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    void Awake() => Invoke("Deactivate", 5f);

    void Deactivate() => gameObject.SetActive(false);
    
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, moveSpeed);
    }
}
