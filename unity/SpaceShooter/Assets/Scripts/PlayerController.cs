using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minPositionOnYAxis, maxPositionOnYAxis;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private float shootPauseTime;
    private float shootTimer;
    private bool canShoot;

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        Vector3 currentPosition = transform.position;
        
        if (Input.GetAxisRaw("Vertical") > 0f)
            currentPosition.y += moveSpeed * Time.deltaTime;
        else if (Input.GetAxisRaw("Vertical") < 0f)
            currentPosition.y -= moveSpeed * Time.deltaTime;
        
        if (currentPosition.y > maxPositionOnYAxis)
            currentPosition.y = maxPositionOnYAxis;
        if (currentPosition.y < minPositionOnYAxis)
            currentPosition.y = minPositionOnYAxis;
        
        transform.position = currentPosition;
    }

    void Shoot()
    {
        shootTimer += Time.deltaTime;
        
        if (shootTimer > shootPauseTime) canShoot = true;
        
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            GetComponent<Animator>().Play("PlayerShoot");
            shootTimer = 0;
            canShoot = false;
            Instantiate(playerBullet, bulletSpawner.position, Quaternion.identity);
            SoundManager.Instance.PlayLaserSFX();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("EnemyBullet") || collider.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Animator>().Play("PlayerDestroy");
            SoundManager.Instance.PlayExplosionSFX();
            Invoke("RestartGame", 2f);
        }
    }

    void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
