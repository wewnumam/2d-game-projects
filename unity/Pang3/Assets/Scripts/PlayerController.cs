using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxVelocity;
    private bool canShoot = true;

    void Update() => Shoot();
    void FixedUpdate() => Walk();

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(ShootTheRocket());
        }
    }

    IEnumerator ShootTheRocket()
    {
        canShoot = false;
        SetAnimationShoot(true);
        
        const float ROCKET_LAUNCH_OFFSET_ON_Y_AXIS = 1f;
        Vector3 currentPosition = transform.position;
        currentPosition.y += ROCKET_LAUNCH_OFFSET_ON_Y_AXIS;
        Instantiate(rocketPrefab, currentPosition, Quaternion.identity);

        SoundManager.Instance.PlayGunShootSFX();

        yield return new WaitForSeconds(0.5f);
        SetAnimationShoot(false);
        canShoot = true;
    }

    void Walk()
    {
        if (IsWalkRight() && !IsExceedMaxVelocity())
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed, 0f));
            FlipSpriteHorizontal();
            SetAnimationWalk(true);
        } 
        else if (IsWalkLeft() && !IsExceedMaxVelocity())
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed, 0f));
            FlipSpriteHorizontal();
            SetAnimationWalk(true);
        }
        else if (IsIdle())
        {
            SetAnimationWalk(false);
        }
    }

    bool IsWalkRight() => Input.GetAxisRaw("Horizontal") > 0;
    bool IsWalkLeft() => Input.GetAxisRaw("Horizontal") < 0;
    bool IsIdle() => Input.GetAxisRaw("Horizontal") == 0;
    bool IsExceedMaxVelocity()
    {
        if (GetComponent<Rigidbody2D>().velocity.x > maxVelocity) return true;
        if (GetComponent<Rigidbody2D>().velocity.x < -maxVelocity) return true;

        return false;
    }

    void FlipSpriteHorizontal()
    {
        Vector3 currentScale = transform.localScale;

        if (IsWalkLeft() && currentScale.x > 0) currentScale.x = -1f;
        else if (IsWalkRight() && currentScale.x < 0) currentScale.x = 1f;
        
        transform.localScale = currentScale;
    }

    void SetAnimationWalk(bool isWalk) => GetComponent<Animator>().SetBool("isWalk", isWalk);
    void SetAnimationShoot(bool isShoot) => GetComponent<Animator>().SetBool("isShoot", isShoot);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }    
    }
}
