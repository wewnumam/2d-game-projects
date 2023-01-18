using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Platform"))
        {
            collider.gameObject.SetActive(false);
        }

        if (collider.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            GameManager.Instance.gameOverPanel.SetActive(true);
        }
    }
}
