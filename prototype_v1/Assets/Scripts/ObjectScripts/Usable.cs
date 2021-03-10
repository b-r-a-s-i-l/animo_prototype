using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Usable : MonoBehaviour
{
    [System.Serializable]
    struct StatusInfluence
    {
        public float value;
        public Bars bars;
    }

    [Header("Options")]
    public bool useApproachZone;
    public bool useEmotionScript;

    [Header("Status Influence")]
    [SerializeField] private StatusInfluence[] status;

    private InventoryManager inventory;
    private PlayerStatus player;
    private EmotionBalloon balloon;

    private bool isTouching;

    //methods get-set
    public bool IsTouching { get => isTouching; set => isTouching = value; }

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
        player = GameManager.Instance.Player.GetComponent<PlayerStatus>();

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
            foreach(StatusInfluence stt in status)
            {
                string barName = "";

                switch (stt.bars)
                {
                    case Bars.Sanity:
                        barName = "SAN";
                        break;
                    case Bars.Hunger:
                        barName = "HUN";
                        break;
                    case Bars.Thirst:
                        barName = "THI";
                        break;
                    case Bars.Fatigue:
                        barName = "FAT";
                        break;
                    case Bars.Toilet:
                        barName = "TOI";
                        break;
                }

                GameManager.Instance.Canvas.SetMensage("Recuperou " + stt.value.ToString() + " em " + barName);
                player.EditPoints(Bars.Fatigue, 100, true);
            }
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
        if (this.enabled == true)
        {
            if (useEmotionScript || !this.IsTouching)
            {
                balloon.CloseAnimate();
            }
        }
    }
}
