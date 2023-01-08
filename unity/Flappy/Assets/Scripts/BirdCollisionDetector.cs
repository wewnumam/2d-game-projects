using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollisionDetector : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            GameManager.Instance.SetGameOver();
        }
    }
}
