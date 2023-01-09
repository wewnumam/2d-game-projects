using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackSpeed;

    private bool isMoveAndAttack;
    private bool isAttackRightSide;

    void Start()
    {
        if (transform.position.x < 0) isAttackRightSide = true;
    }
    
    void Update()
    {
        BeeAttack();
    }

    void BeeAttack()
    {
        if (IsReachYAxisToAttack())
        {
            StartCoroutine(AttackPlayer());
        }
        else
        {
            Vector3 currentPosition = transform.position;
            currentPosition.y -= moveSpeed * Time.deltaTime;

            transform.position = currentPosition;
        }

        if (!isMoveAndAttack) return;
        
        if (isAttackRightSide)
            transform.position += Vector3.right * attackSpeed * Time.deltaTime;
        else
            transform.position += Vector3.left * attackSpeed * Time.deltaTime;
    }

    bool IsReachYAxisToAttack()
    {
        const float POSITION_ON_Y_AXIS_TO_ATTACK = 0;
        return transform.position.y < POSITION_ON_Y_AXIS_TO_ATTACK;
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(1.5f);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, -45f)));
        Invoke("Deactivate", 5f);
        isMoveAndAttack = true;
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
