using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private GameObject[] sideBounds;
    private float cameraY;
    private float boundHeight; 

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] spawnPosition;

    void Awake()
    {
        sideBounds = GameObject.FindGameObjectsWithTag("SideBound");
        cameraY = Camera.main.gameObject.transform.position.y;

        boundHeight = GetComponent<BoxCollider2D>().bounds.size.y;
    }

    void Update()
    {
        Move();
        Reposition();
    }

    void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y -= moveSpeed * Time.deltaTime;

        transform.position = currentPosition;
    }

    void Reposition()
    {
        const float CAMERA_SPACE_BEFORE_REPOSITION = 15f;
        if (transform.position.y >= cameraY - CAMERA_SPACE_BEFORE_REPOSITION) return;
       
        float highestBoundsY = sideBounds[0].transform.position.y;
        
        for (int i = 0; i < sideBounds.Length; i++)
        {
            if (highestBoundsY < sideBounds[i].transform.position.y)
            {
                highestBoundsY = sideBounds[i].transform.position.y;
            }
        }

        Vector3 currentPosition = transform.position;
        const float FILL_GAP_BETWEEN_BOUND = -0.4f;
        currentPosition.y = highestBoundsY + boundHeight + FILL_GAP_BETWEEN_BOUND;

        transform.position = currentPosition;
        
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        const int FREQUENCY_OF_SPAWNING_ENEMY = 0;
        if (Random.Range(0, 10) <= FREQUENCY_OF_SPAWNING_ENEMY) return;
        
        int randomEnemyIndex = Random.Range(0, enemies.Length);

        if (IsFlagEnemy(randomEnemyIndex))
        {
            Instantiate(
                original: enemies[randomEnemyIndex],
                position: new Vector3(0f, transform.position.y, 3f),
                rotation: Quaternion.identity
            );
        }
        else
        {
            GameObject enemyObject = Instantiate(enemies[randomEnemyIndex]);
            Vector3 enemyScale = enemyObject.transform.localScale;

            const int RIGHT_SPAWNER_INDEX = 0;
            const int LEFT_SPAWNER_INDEX = 1;

            const int FREQUENCY_OF_SPAWNING_POSITION_RIGHT = 0;
            if (Random.Range(0, 2) > FREQUENCY_OF_SPAWNING_POSITION_RIGHT)
            {
                enemyObject.transform.position = spawnPosition[RIGHT_SPAWNER_INDEX].transform.position;
                enemyScale.x = -Mathf.Abs(enemyScale.x);
            }
            else
            {
                enemyObject.transform.position = spawnPosition[LEFT_SPAWNER_INDEX].transform.position;
                enemyScale.x = Mathf.Abs(enemyScale.x);
            }

            enemyObject.transform.localScale = enemyScale;
        }
    }

    bool IsFlagEnemy(int enemyIndex)
    {
        const int FLAG_ENEMY_INDEX = 0;
        return enemyIndex == FLAG_ENEMY_INDEX;
    }
}
