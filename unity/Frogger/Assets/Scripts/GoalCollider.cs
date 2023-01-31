using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalCollider : MonoBehaviour
{
    private GameObject frog;
    private Vector2 frogInitialPosition;
    private int score;
    [SerializeField] private Text scoreText;

    void Awake()
    {
        frog = GameObject.FindWithTag("Player");
        frogInitialPosition = frog.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.transform.position = frogInitialPosition;

            score += 100;
            scoreText.text = score.ToString();
        }    
    }
}
