using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;

    public void SpawnBox()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.z = 10f;
        Instantiate(boxPrefab, currentPosition, Quaternion.identity);
        GameManager.Instance.AddMoveCount();
    }
}
