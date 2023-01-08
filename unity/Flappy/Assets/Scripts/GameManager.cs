using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    
    private int _score;
    public int Score { get => _score; set => _score = value; }

    private bool _isGameOver;
    public bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }

    private static float timer;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        DelayGameOver();
    }

    public void AddScore() => _score++;

    public void SetGameOver() => _isGameOver = true;

    void DelayGameOver()
    {
        timer += Time.deltaTime;
        const float PAUSE_TIME = 2;

        if (timer > PAUSE_TIME)
        {
            timer = 0;
            if (_isGameOver)
            {
                _isGameOver = false;
                _score = 0;
                SceneManager.LoadScene("Game");
            }
        }

    }
}
