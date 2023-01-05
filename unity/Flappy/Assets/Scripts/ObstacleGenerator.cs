using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private float timer;
    [SerializeField] private float pause;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float maxYAxis;
    [SerializeField] private float minYAxis;

    /// <summary>
    /// Generate obstacles at random y-axis position with a delay.
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > pause)
        {
            float randomYAxis = Random.Range(minYAxis, maxYAxis);
            Instantiate(
                original: obstaclePrefab,
                position: new Vector2(transform.position.x, randomYAxis),
                rotation: transform.rotation);
            timer = 0;
        }
    }
}
