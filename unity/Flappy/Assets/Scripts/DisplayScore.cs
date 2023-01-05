using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = GameManager.Score.ToString();
    }
}
