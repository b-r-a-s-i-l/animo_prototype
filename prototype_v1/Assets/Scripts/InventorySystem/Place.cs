using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Place : MonoBehaviour
{
    [Header("Identify")]
    [SerializeField] private Item itemNeeded;
    private string iD;
    [SerializeField] private bool useUsableScript;
    [SerializeField] private Sprite useAnotherSprite;


    private InventoryManager inventory;
    private GameObject holdItem;
    private Collider2D place;
    private SpriteRenderer spriteRenderer;

    private bool isTouching;
    private bool seted;

    //methods get-set
    public Item ItemNeeded { get => itemNeeded; set => itemNeeded = value; }
    public Sprite Sprite { get => useAnotherSprite; set => useAnotherSprite = value; }
    public string ID { get => iD; set => iD = value; }
    public bool UsableScript { get => useUsableScript; set => useUsableScript = value; }
    public bool IsTouching { get => isTouching; set => isTouching = value; }
    public bool Seted { get => seted; set => seted = value; }

    private void Start()
    {
        ID = itemNeeded.ID;

        if (UsableScript) gameObject.GetComponent<Usable>().enabled = false;

        inventory = GameManager.Instance.Inventory;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        place = gameObject.GetComponent<Collider2D>();

        spriteRenderer.enabled = false;
        place.enabled = false;
    }

    private void FixedUpdate()
    {
        holdItem = GameManager.Instance.Inventory.HoldMouseItem;
    }

    private void Update()
    {
        if (!seted)
        {
            if (holdItem != null)
            {
                if (holdItem.GetComponent<Item>().ID == this.ID)
                {
                    spriteRenderer.enabled = true;
                    place.enabled = true;
                }
            }
            else
            {
                spriteRenderer.enabled = false;
                place.enabled = false;
            }
        }
        else
        {
            if (UsableScript)
            {
                gameObject.GetComponent<Usable>().enabled = true;
                gameObject.GetComponent<Place>().enabled = false;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (place.enabled == true && !seted)
        {
            holdItem.GetComponent<Image>().color = new Vector4(0, 255, 0, 0.5f);
        }
    }

    private void OnMouseExit()
    {
        if (holdItem != null && !seted)
        {
            holdItem.GetComponent<Image>().color = new Vector4(255, 255, 255, 1);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && !seted)
        {
            Destroy(holdItem);
            spriteRenderer.color = new Color(255, 255, 255, 1);
            if (Sprite != null) spriteRenderer.sprite = Sprite;

            seted = true;
        }
    }
}
