using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject rocketPrefab, planeEnemyPrefab;
    [SerializeField] private bool isSpawnRocket, isSpawnPlaneEnemy;
    [SerializeField] private float spawnTime;

    void Start() => Invoke("SpawnEnemy", spawnTime);

    void SpawnEnemy()
    {
        GameObject enemyInstance = null;
        if (isSpawnRocket)
            enemyInstance = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
        if (isSpawnPlaneEnemy)
            enemyInstance = Instantiate(planeEnemyPrefab, transform.position, Quaternion.identity);
        
        enemyInstance.GetComponent<EnemyController>().enemySpawner = this;
    }

    public void ActivateSpawning() => Invoke("SpawnEnemy", spawnTime);
}
