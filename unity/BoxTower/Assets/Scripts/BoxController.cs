using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private float minMovePositionOnXAxis, maxMovePositionOnXAxis;
    [SerializeField] private float moveSpeed;

    private bool isMove = true;
    private bool isGameOver;
    private bool isIgnoreCollision;
    private bool isIgnoreTrigger;

    void Awake()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GameManager.Instance.currentBox = this;
    }

    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (isMove)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x += moveSpeed * Time.deltaTime;

            if (IsHitBoundsOnXAxis(currentPosition)) moveSpeed *= -1f;

            transform.position = currentPosition;
        }
    }

    bool IsHitBoundsOnXAxis(Vector3 currentPosition)
    {
        return currentPosition.x > maxMovePositionOnXAxis || currentPosition.x < minMovePositionOnXAxis;
    }

    public void DropBox()
    {
        isMove = false;
        GetComponent<Rigidbody2D>().gravityScale = (float)Random.Range(2, 4);
    }

    void Landed()
    {
        if (isGameOver) return;

        isIgnoreCollision = true;
        isIgnoreTrigger = true;

        GameManager.Instance.SpawnNewBox();
        GameManager.Instance.MoveCamera();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isIgnoreCollision || isMove) return;

        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Box"))
        {
            Invoke("Landed", 0.5f);
            isIgnoreCollision = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isIgnoreTrigger || isMove) return;

        if (collider.gameObject.CompareTag("GameOver"))
        {
            CancelInvoke("Landed");
            isIgnoreTrigger = true;
            isGameOver = true;

            Invoke("RestartGame", 2f);
        }
    }

    void RestartGame() => GameManager.Instance.RestartGame();
}
