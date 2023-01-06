using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    void Update() => Scroll();

    void Scroll()
    {
        Vector2 offset = GetComponent<MeshRenderer>().sharedMaterial.GetTextureOffset("_MainTex");
        offset.y += Time.deltaTime * scrollSpeed;

        GetComponent<MeshRenderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
