using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] GameObject nextBallPrefab;
    [SerializeField] bool isSmallestBall;

    void Awake()
    {
        const float MAX_VELOCITY = -5f;
        const float MIN_VELOCITY = 6f;
        float randomDirectionOnXAxis = Random.Range(MAX_VELOCITY, MIN_VELOCITY);
        GetComponent<Rigidbody2D>().velocity = new Vector2(randomDirectionOnXAxis, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rocket"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            if (!isSmallestBall)
            {
                SoundManager.Instance.PlayBallExplosionSFX();
                Instantiate(nextBallPrefab, transform.position, Quaternion.identity);
                Instantiate(nextBallPrefab, transform.position, Quaternion.identity);
            }
        }    
    }
}
