using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Vector2 playerInitialPosition;
    private GameObject[] enemies;
    private GameObject player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerDied()
    {
        Time.timeScale = 0f;

        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerReachedGoal()
    {
        player.transform.position = playerInitialPosition;
        player.GetComponent<PlayerController>().moveSpeed += 0.3f;

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().moveSpeed += 1f;
        }
    }
}
