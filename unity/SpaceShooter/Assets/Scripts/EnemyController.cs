using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform bulletSpawner;
    private float shootTimer;
    private float shootPauseTime;
    private const float MIN_SHOOT_PAUSE_TIME = 0.5f;
    private const float MAX_SHOOT_PAUSE_TIME = 3f;

    void Start()
    {
        Invoke("Deactivate", 15f);
        shootPauseTime = Random.Range(MIN_SHOOT_PAUSE_TIME, MAX_SHOOT_PAUSE_TIME);
    } 

    void Update()
    {
        Move();
        Shoot();
    }

    void Deactivate() => gameObject.SetActive(false);

    void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x -= moveSpeed * Time.deltaTime;
        
        transform.position = currentPosition;
    }

    void Shoot()
    {
        shootTimer += Time.deltaTime;
        
        if (shootTimer > shootPauseTime)
        {
            shootTimer = 0;
            Instantiate(enemyBullet, bulletSpawner.position, Quaternion.identity);
            shootPauseTime = Random.Range(MIN_SHOOT_PAUSE_TIME, MAX_SHOOT_PAUSE_TIME); 
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerBullet"))
        {
            GetComponent<Animator>().Play("EnemyDestroy");
            collider.gameObject.SetActive(false);
            SoundManager.Instance.PlayExplosionSFX();
        }
    }
}
