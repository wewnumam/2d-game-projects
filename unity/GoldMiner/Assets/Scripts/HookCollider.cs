using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCollider : MonoBehaviour
{
    [SerializeField] private Transform itemHolderPosition;
    private bool isHold;

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!isHold && (collider.gameObject.CompareTag(Tags.GOLD) || collider.gameObject.CompareTag(Tags.DELIVER_ITEM) || collider.gameObject.CompareTag(Tags.STONE)))
        {
            StartCoroutine(CollectItem(collider.gameObject));
        }    
    }

    IEnumerator CollectItem(GameObject item)
    {
        isHold = true;
        item.transform.SetParent(itemHolderPosition);
        yield return new WaitForSeconds(3f);
        item.SetActive(false);
        isHold = false;
    } 
}
