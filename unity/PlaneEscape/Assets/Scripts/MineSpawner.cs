using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private float minSpawnPositionOnXAxis, maxSpawnPositionOnXAxis;
    [SerializeField] private float spawnInterval;

    void Start() => Invoke("ActivateMineSpawner", 1f);

    void ActivateMineSpawner()
    {
        StartCoroutine(SpawnMines());
        Invoke("ActivateMineSpawner", spawnInterval);
    }

    IEnumerator SpawnMines()
    {
        const int MIN_MINE_AMOUNT = 3;
        const int MAX_MINE_AMOUNT = 8;
        int count = Random.Range(MIN_MINE_AMOUNT, MAX_MINE_AMOUNT);

        Vector3 currentPosition = transform.position;

        for (int i = 0; i < count; i++)
        {
            currentPosition.x = Random.Range(minSpawnPositionOnXAxis, maxSpawnPositionOnXAxis);
            Instantiate(minePrefab, currentPosition, Quaternion.identity);

            yield return null;
        }
    }
}
