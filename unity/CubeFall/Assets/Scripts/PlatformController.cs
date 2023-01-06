using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float topBoundsOnYAxis;
    [SerializeField] private bool isStandardPlaform, isBreakablePlatform, isSpikePlatform, isSpeedPlaformLeft, isSpeedPlaformRight;
    
    void Update()
    {
        MovePlatformOnYAxis();
        SelfDestroyOnBoundsYAxis();
    }

    void MovePlatformOnYAxis()
    {
        Vector2 currentPosition = transform.position;
        currentPosition.y += moveSpeed * Time.deltaTime;

        transform.position = currentPosition;
    }

    void SelfDestroyOnBoundsYAxis()
    {
        if (transform.position.y >= topBoundsOnYAxis) DestroyPlatform();
    }

    void DestroyPlatform() => Destroy(gameObject);

    void DestroyBreakablePlatform()
    {
        const float BREAKABLE_PLARFORM_BREAK_TIME = 0.5f;
        Invoke("DestroyPlatform", BREAKABLE_PLARFORM_BREAK_TIME);
        SoundManager.Instance.PlayIceBreakSFX();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player")) return;

        if (isSpikePlatform)
        {
            SoundManager.Instance.PlayDeathSFX();
            Destroy(collider.gameObject);

            GameManager.Instance.RestartGame();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        SoundManager.Instance.PlayLandSFX();

        if (isBreakablePlatform)
        {
            GetComponent<Animator>().Play("BreakablePlatformBreak");
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        const float PLAYER_HORIZONTAL_MOVE_SPEED = 1f;
        if (isSpeedPlaformRight)
        {
            collision.gameObject.GetComponent<PlayerController>().MoveOnSpeedPlatform(PLAYER_HORIZONTAL_MOVE_SPEED);
        }
        if (isSpeedPlaformLeft)
        {
            collision.gameObject.GetComponent<PlayerController>().MoveOnSpeedPlatform(-PLAYER_HORIZONTAL_MOVE_SPEED);
        }
    }
}
