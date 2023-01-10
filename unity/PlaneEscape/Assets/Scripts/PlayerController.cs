using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject explosion;
    private float horizontal;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Make coordinate relative to itself not Unity
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Mine"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Invoke("RestartGame", 2f);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }    
    }

    void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
