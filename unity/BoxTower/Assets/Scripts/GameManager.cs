using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    [SerializeField] private BoxSpawner boxSpawner;
    [SerializeField] private CameraFollow cameraFollow;
    [HideInInspector] public BoxController currentBox;
    private int moveCount;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    void Start() => boxSpawner.SpawnBox();

    void Update()
    {
        DetectInput();
    }

    void DetectInput()
    {
        if (Input.GetMouseButtonDown(0)) currentBox.DropBox();
    }

    public void SpawnNewBox() => Invoke("SpawnBox", 0.5f);

    void SpawnBox() => boxSpawner.SpawnBox();

    public void MoveCamera()
    {
        if (moveCount == 3)
        {
            moveCount = 0;
            const float CAMERA_MOVE_ON_Y_AXIS = 2f;
            cameraFollow.targetPosition.y += CAMERA_MOVE_ON_Y_AXIS;
        }
    }

    public void AddMoveCount() => moveCount++;

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
