using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    private bool isAttackRightSide;

    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-15f, 15f)));
        
        if (transform.position.x < 0) isAttackRightSide = true;

        Invoke("Deactivate", 5f);
    }

    void Update()
    {
        ShurikenAttack();
    }

    void ShurikenAttack()
    {
        if (isAttackRightSide)
            transform.position += Vector3.right * attackSpeed * Time.deltaTime;
        else
            transform.position += Vector3.left * attackSpeed * Time.deltaTime;
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
