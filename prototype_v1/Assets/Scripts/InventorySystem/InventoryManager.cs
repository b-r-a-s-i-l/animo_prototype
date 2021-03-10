using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject[] slots;
    private GameObject[] places;
    [SerializeField] private GameObject holdMouseItem;

    //methods get-set
    public GameObject HoldMouseItem { get => holdMouseItem; set => holdMouseItem = value; }

    void Start()
    {
        slots = GameObject.FindGameObjectsWithTag("Slot");
        places = GameObject.FindGameObjectsWithTag("Place");
    }

    public void ReceiveItem(GameObject item)
    {
        if (item.tag == "Item")
        {
            foreach (GameObject slot in slots)
            {
                if (slot.transform.childCount == 0)
                {
                    GameManager.Instance.Canvas.SetMensage("Pegou " + item.GetComponent<Item>().ID);
                    slot.GetComponent<Slot>().SetItemToSlot(item);
                    return;
                }
            }
        }
    }

    public void DragItemOfInventory(GameObject item)
    {
        if (item != holdMouseItem)
        {
            holdMouseItem = item;
        }

        if (holdMouseItem != null)
        {
            holdMouseItem.transform.position = Input.mousePosition;
        }
    }
}
