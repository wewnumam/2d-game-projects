using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    void Update()
    {
        if (IsReachPositionToDeactive()) gameObject.SetActive(false);
    }

    bool IsReachPositionToDeactive()
    {
        const float POSITION_TO_DEACTIVATE_ON_Y_AXIS = -5f;
        return transform.position.y < POSITION_TO_DEACTIVATE_ON_Y_AXIS;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }    
    }
}
