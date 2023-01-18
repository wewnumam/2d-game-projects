using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce; 
    private bool hasJumped, platformBound;
    private Button jumpButton;
    private GameObject parent;
    
    public delegate void MoveCamera();
    public static event MoveCamera move;

    void Awake()
    {
        jumpButton = GameObject.Find("JumpButton").GetComponent<Button>();
        jumpButton.onClick.AddListener(() => Jump());
    }

    void Update()
    {
        if (hasJumped && GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            if (!platformBound)
            {
                hasJumped = false;
                transform.SetParent(parent.transform);
                GameManager.Instance.CreatePlatform();
                if (move != null) move();
                GameManager.Instance.score++;
                GameManager.Instance.scoreText.text = GameManager.Instance.score.ToString();
            }
            else if (parent != null) transform.SetParent(parent.transform);
        }
    }

    void Jump()
    {
        if (GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
            transform.SetParent(null);
            hasJumped = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            parent = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            parent = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("MainCamera"))
        {
            platformBound = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("MainCamera"))
        {
            platformBound = false;
        }
    }
}
