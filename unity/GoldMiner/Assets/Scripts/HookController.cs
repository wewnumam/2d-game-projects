using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private float minRotationZ, maxRotationZ;
    [SerializeField] private float rotateSpeed;
    private float rotateAngle;
    private bool isRotateRight;
    private bool canRotate;
    [SerializeField] private float moveSpeed;
    private float initialMoveSpeed;
    [SerializeField] private float minYPosition;
    private float initialYPosition;
    private bool isMoveDown;

    private RopeRenderer ropeRenderer;

    void Awake() => ropeRenderer = GetComponent<RopeRenderer>();

    void Start()
    {
        initialYPosition = transform.position.y;
        initialMoveSpeed = moveSpeed;
        canRotate = true;
    }

    void Update()
    {
        Rotate();
        GetInput();
        MoveRope();
    }

    void Rotate()
    {
        if (!canRotate) return;

        if (isRotateRight)
            rotateAngle += rotateSpeed * Time.deltaTime;
        else
            rotateAngle -= rotateSpeed * Time.deltaTime;
        
        transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);

        if (rotateAngle >= maxRotationZ)
            isRotateRight = false;
        else if (rotateAngle <= minRotationZ)
            isRotateRight = true;
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0) && canRotate)
        {
            canRotate = false;
            isMoveDown = true;
        SoundManager.Instance.PlayRopeStretchSFX();
        }
    }

    void MoveRope()
    {
        if (canRotate) return;


        Vector3 currentPosition = transform.position;

        if (isMoveDown)
            currentPosition -= transform.up * Time.deltaTime * moveSpeed;
        else
            currentPosition += transform.up * Time.deltaTime * moveSpeed;

        transform.position = currentPosition;

        if (currentPosition.y <= minYPosition)
            isMoveDown = false;
        
        if (currentPosition.y >= initialYPosition)
        {
            canRotate = true;
            ropeRenderer.RenderLine(currentPosition, false);
            moveSpeed = initialMoveSpeed;
        }

        ropeRenderer.RenderLine(currentPosition, true);
    }
}
