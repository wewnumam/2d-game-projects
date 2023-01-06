using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    private float timer;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float minXAxis;
    [SerializeField] private float maxXAxis;

    void Update()
    {
        SpawnPlatform();
    }

    void SpawnPlatform()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            int randomPlatfromPrefabIndex = Random.Range(0, platformPrefabs.Length - 1);
            float randomXAxis = Random.Range(minXAxis, maxXAxis);
            Instantiate(
                original: platformPrefabs[randomPlatfromPrefabIndex],
                position: new Vector2(randomXAxis, transform.position.y),
                rotation: transform.rotation);
            timer = 0;
        }
    }
}
