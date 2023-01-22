using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner Instance { get; private set; }
    [SerializeField] private GameObject[] blocks;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SpawnRandom();
    }

    public void SpawnRandom()
    {
        int blockIndex = Random.Range(0, blocks.Length);
        Instantiate(blocks[blockIndex], transform.position, Quaternion.identity);
    }
}
