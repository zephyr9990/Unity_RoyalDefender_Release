using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public GameObject[] items;
    
    public void DropItem()
    {
        int itemIndex = Random.Range(0, items.Length);
        if (itemIndex < items.Length)
        {
            Instantiate(items[itemIndex], gameObject.transform.position, Quaternion.identity);
        }
    }
}
