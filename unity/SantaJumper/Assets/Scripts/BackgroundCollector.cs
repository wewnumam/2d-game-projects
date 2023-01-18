using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCollector : MonoBehaviour
{
    private GameObject[] backgrounds;
    private float firstY;

    void Awake()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        firstY = backgrounds[0].transform.position.y;

        for (int i = 1; i < backgrounds.Length; i++)
        {
            if (firstY < backgrounds[i].transform.position.y)
            {
                firstY = backgrounds[i].transform.position.y;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Background"))
        {
            Vector3 currentPosition = transform.position;
            const float FILL_GAP = 2.5f;
            float height = ((BoxCollider2D)collider).size.y + FILL_GAP;
            currentPosition.y = firstY + height;
            currentPosition.z = 0f;
            collider.transform.position = currentPosition;
            firstY = currentPosition.y; 
        }
    }
}
