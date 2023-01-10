using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDeactivator : MonoBehaviour
{
    void Start() => Invoke("Deactivate", 2f);

    void Deactivate() => gameObject.SetActive(false);
}
