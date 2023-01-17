using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance;}
    [HideInInspector] public string KNIFE_TAG = "Knife"; 
    public bool isGameOver;
    private float timerCount;
    [SerializeField] private Text timeText;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timerCount = 0;
        StartCoroutine(CountTime());
    }

    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(1f);

        if (!isGameOver) timerCount++;
        timeText.text = $"Time: {timerCount}";
        StartCoroutine(CountTime());
    }

    public void RestartGame()
    {
        isGameOver = true;
        Invoke("ReloadScene", 2f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
