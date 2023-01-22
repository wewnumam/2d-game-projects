using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Transform buttonParent;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private int buttonAmount;
    
    void Awake()
    {
        for (int i = 0; i < buttonAmount; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.name = i.ToString();
            button.transform.SetParent(buttonParent, false);
        }
    }
}
