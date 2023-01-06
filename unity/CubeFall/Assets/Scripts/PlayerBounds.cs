using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    [SerializeField] private float minBoundsOnXAxis;
    [SerializeField] private float maxBoundsOnXAxis;
    [SerializeField] private float bottomBoundsOnYAxis;

    // Update is called once per frame
    void Update()
    {
        KeepPlayerOnSideBounds();
        CheckPlayerOutOfYAxisBounds();
    }

    void KeepPlayerOnSideBounds()
    {
        Vector2 currentPosition = transform.position;

        if (currentPosition.x > maxBoundsOnXAxis) currentPosition.x = maxBoundsOnXAxis;
        if (currentPosition.x < minBoundsOnXAxis) currentPosition.x = minBoundsOnXAxis;

        transform.position = currentPosition;
    }

    void CheckPlayerOutOfYAxisBounds()
    {
        if (transform.position.y <= bottomBoundsOnYAxis)
        {
            GameManager.Instance.RestartGame();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TopSpike"))
        {
            SoundManager.Instance.PlayDeathSFX();
            Destroy(gameObject);

            GameManager.Instance.RestartGame();
        }
    }
}
