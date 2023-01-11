using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float scrollOnXAxis;

    void Update()
    {
        Scroll();    
    }

    void Scroll()
    {
        scrollOnXAxis = moveSpeed * Time.time;
        Vector2 offset = new Vector2(scrollOnXAxis, 0f);
        GetComponent<MeshRenderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
