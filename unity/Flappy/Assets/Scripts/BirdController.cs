using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float RotateUpSpeed; 
    [SerializeField] private float RotateDownSpeed;

    enum BirdYAxisTravelState
    {
        GoingUp, GoingDown
    }

    private BirdYAxisTravelState birdYAxisTravelState;
    private Vector3 birdRotation = Vector3.zero;

    void Update()
    {
        if (IsTouchedOrClicked() && !GameManager.Instance.IsGameOver) BoostOnYAxis();
        
        BirdRotation();
    }

    bool IsTouchedOrClicked()
    {
        bool isTouchedOrClicked = false;

        if (Input.GetButtonUp("Jump") || 
            Input.GetMouseButtonDown(0) || 
            (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)) 
            {
                isTouchedOrClicked = true;
            }
        
        return isTouchedOrClicked;
    }

    void BoostOnYAxis() 
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
    }

    void BirdRotation()
    {
        birdYAxisTravelState = GetComponent<Rigidbody2D>().velocity.y > 0 ? BirdYAxisTravelState.GoingUp : BirdYAxisTravelState.GoingDown;

        float degreesToAdd = 0;

        switch (birdYAxisTravelState)
        {
            case BirdYAxisTravelState.GoingUp:
                degreesToAdd = 6 * RotateUpSpeed;
                break;
            case BirdYAxisTravelState.GoingDown:
                degreesToAdd = -3 * RotateDownSpeed;
                break;
            default:
                break;
        }

        birdRotation = new Vector3(0, 0, Mathf.Clamp(birdRotation.z + degreesToAdd, -30, 30));
        transform.eulerAngles = birdRotation;
    }
}


