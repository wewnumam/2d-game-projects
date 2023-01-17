using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;
    const float MIN_SPAWN_TIME_IN_SECONDS = 1f;
    const float MAX_SPAWN_TIME_IN_SECONDS = 2f;
    const float MIN_SPAWN_POSITION_ON_Y_AXIS = -4f;
    const float MAX_SPAWN_POSITION_ON_Y_AXIS = 4.3f;

    void Start()
    {
        StartCoroutine(SpawnKnife());
    }

    IEnumerator SpawnKnife()
    {
        yield return new WaitForSeconds(Random.Range(MIN_SPAWN_TIME_IN_SECONDS, MAX_SPAWN_TIME_IN_SECONDS));

        Instantiate(
            original: knifePrefab,
            position: new Vector3(transform.position.x, Random.Range(MIN_SPAWN_POSITION_ON_Y_AXIS, MAX_SPAWN_POSITION_ON_Y_AXIS), 0f),
            rotation: Quaternion.identity);
        
        StartCoroutine(SpawnKnife());
    }
}
