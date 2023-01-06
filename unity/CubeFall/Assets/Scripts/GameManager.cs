using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }
    
    public void RestartGame()
    {
        const float RESTART_DELAY_TIME = 2f;
        Invoke("RestartGameAfterTime", RESTART_DELAY_TIME);
    }

    void RestartGameAfterTime()
    {
        SceneManager.LoadScene("Game");
    }
}
