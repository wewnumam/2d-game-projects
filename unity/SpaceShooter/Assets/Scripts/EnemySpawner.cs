using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] private float minPositionOnYAxis, maxPositionOnYAxis;
    private float spawnTimer;
    private float spawnPauseTime;
    private const float MIN_SPAWN_PAUSE_TIME = 1f;
    private const float MAX_SPAWN_PAUSE_TIME = 5f;

    void Start() => spawnPauseTime = Random.Range(MIN_SPAWN_PAUSE_TIME, MAX_SPAWN_PAUSE_TIME);
    void Update() => SpawnEnemy();

    void SpawnEnemy()
    {
        spawnTimer += Time.deltaTime;
        
        if(spawnTimer > spawnPauseTime)
        {
            spawnTimer = 0;

            int randomEnemyIndex = Random.Range(0, enemies.Length);
            
            Vector3 currentPosition = transform.position;
            float randomSpawnPositionOnYAxis = Random.Range(minPositionOnYAxis, maxPositionOnYAxis);
            currentPosition.y = randomSpawnPositionOnYAxis;

            Instantiate(original: enemies[randomEnemyIndex],
                        position: currentPosition,
                        rotation: Quaternion.Euler(0f, 0f, 90f));

            spawnPauseTime = Random.Range(MIN_SPAWN_PAUSE_TIME, MAX_SPAWN_PAUSE_TIME); 
        }
    }
}
