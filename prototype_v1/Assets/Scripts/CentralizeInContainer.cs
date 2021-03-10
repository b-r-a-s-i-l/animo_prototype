using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralizeInContainer : MonoBehaviour
{
    private InventoryManager inventory;
    private Transform item;

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    private void Update()
    {
        if (transform.childCount > 0)
        {
            item = transform.GetChild(0);
            item.position += (transform.position - item.position) * 5 * Time.deltaTime;
        }
    }
}
