using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    private InventoryManager inventory;

    [SerializeField] private GameObject slotItemUI;
    private RectTransform rectTransform;
    private Image image;
    private Canvas canvas;
    private Item itemScript;

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    public void SetItemToSlot(GameObject item)
    {
        slotItemUI = new GameObject(item.GetComponent<Item>().ID);
        slotItemUI.transform.SetParent(gameObject.transform);

        rectTransform = slotItemUI.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;

        slotItemUI.AddComponent<CanvasRenderer>();

        image = slotItemUI.AddComponent<Image>();
        image.sprite = item.GetComponent<Item>().Sprite;
        image.preserveAspect = true;

        canvas = slotItemUI.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 10;

        slotItemUI.AddComponent<GraphicRaycaster>();

        itemScript = slotItemUI.AddComponent<Item>();
        itemScript.ID = item.GetComponent<Item>().ID;
        itemScript.Type = item.GetComponent<Item>().Type;
        itemScript.Sprite = item.GetComponent<Item>().Sprite;

        Destroy(item);
    }

    public void CatchItem()
    {  
        if (slotItemUI != null)
        {
            inventory.HoldMouseItem = slotItemUI;
        }
    }

    public void DragItem()
    {
        if (slotItemUI != null)
        {
            inventory.DragItemOfInventory(slotItemUI);
        }
    }

    public void SetCanvasSortingOrderOfItem(int value)
    {
        if (slotItemUI != null)
        {
            canvas.sortingOrder = value;
        }
    }
    
    public void GiveBackItem()
    {
        if (slotItemUI != null)
        {
            inventory.HoldMouseItem = null;
        }
    }
}
