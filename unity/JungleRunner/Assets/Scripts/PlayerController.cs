using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private bool isAlive = true;
    private bool isOnRight = true;
    private bool isOnLeft;
    private bool isJump;

    void Awake()
    {
        GameObject.Find("JumpButton").GetComponent<Button>().onClick.AddListener(Jump);
    }

    void Update()
    {
        if (isJump || !isAlive) return;

        if (isOnRight)
            GetComponent<Animator>().Play("PlayerRunRight");
        if (isOnLeft)
            GetComponent<Animator>().Play("PlayerRunLeft");
    }

    public void Jump()
    {
        if (!isAlive) return;

        if (isOnRight)
            GetComponent<Animator>().Play("PlayerJumpLeft");
        if (isOnLeft)
            GetComponent<Animator>().Play("PlayerJumpRight");

        isJump = true;
        SoundManager.Instance.PlayJumpSFX();
    }

    void OnRight()
    {
        isOnRight = true;
        isOnLeft = false;
        isJump = false;
    }

    void OnLeft()
    {
        isOnRight = false;
        isOnLeft = true;
        isJump = false;
    }

    void PlayerDied()
    {
        SoundManager.Instance.PlayDeathSFX();
        isAlive = false;
        if (isOnRight)
            GetComponent<Animator>().Play("PlayerDiedRight");
        if (isOnLeft)
            GetComponent<Animator>().Play("PlayerDiedLeft");
        
        GameManager.Instance.GameOver();
        Time.timeScale = 0f;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Player jump to kill enemy
        if (isJump)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.SetActive(false);
                SoundManager.Instance.PlayKillSFX();
            }
        }
        else
        {
            if (collider.gameObject.CompareTag("Enemy")) PlayerDied();
        }

        if (collider.gameObject.CompareTag("EnemyTree")) PlayerDied();
    }
}
