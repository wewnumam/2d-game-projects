using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float distanceBetweenPlatform;
    private int platfromCount;
    private float lastPlatformPositionOnYAxis;
    public GameObject gameOverPanel;
    public int score;
    public Text scoreText; 

    void Awake()
    {
        gameOverPanel.SetActive(false);
        MakeSingleton();
        CreatePlatform();
    }

    void MakeSingleton()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    void OnDisable() => _instance = null;

    public void CreatePlatform()
    {
        lastPlatformPositionOnYAxis += distanceBetweenPlatform;
        Instantiate(platformPrefab, new Vector3(0f, lastPlatformPositionOnYAxis, 0f), Quaternion.identity);
        platfromCount++;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
