using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    
    private static int _score;
    public static int Score { get => _score; set => _score = value; }

    private static bool _isGameOver;
    public static bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }

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

    public static void AddScore() => Score++;

    public static void SetGameOver() => IsGameOver = true;

    void DelayGameOver()
    {
        timer += Time.deltaTime;
        const float PAUSE_TIME = 2;

        if (timer > PAUSE_TIME)
        {
            timer = 0;
            if (IsGameOver)
            {
                IsGameOver = false;
                Score = 0;
                SceneManager.LoadScene("Game");
            }
        }

    }
}
