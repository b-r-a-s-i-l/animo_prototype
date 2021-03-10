using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { None, Usable, Drinkable, Eatable, Combinable }

[System.Serializable]
public class Item : MonoBehaviour
{
    [Header("Identify")]
    [SerializeField] private string iD;
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite spriteForUI;

    [Header("Options")]
    public bool useApproachZone;
    public bool useEmotionScript;

    private InventoryManager inventory;
    private EmotionBalloon balloon;

    private bool isTouching;

    //methods get-set
    public string ID { get => iD; set => iD = value; }
    public ItemType Type { get => type; set => type = value; }
    public Sprite Sprite { get => spriteForUI; set => spriteForUI = value; }
    public bool IsTouching { get => isTouching; set => isTouching = value; }

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;

        if (useApproachZone)
        {
            ApproachZone zone = new ApproachZone();
            zone.CreateApproachZone(this.gameObject);
            this.IsTouching = false;
        }

        if (useEmotionScript)
        {
            balloon = gameObject.GetComponent<EmotionBalloon>();
            balloon.CreateBalloon();
        }
    }

    private void OnMouseDown()
    {
        if (this.IsTouching)
        {
            inventory.ReceiveItem(gameObject);
        }
    }

    private void OnMouseOver()
    {
        if (this.IsTouching)
        {
            if (useEmotionScript)
            {
                balloon.OpenAnimate();
            }
        }
    }

    private void OnMouseExit()
    {
        if (useEmotionScript || !this.IsTouching)
        {
            balloon.CloseAnimate();
        }
    }
}