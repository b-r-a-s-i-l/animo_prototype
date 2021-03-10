using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Item Catch Mensage")]
    public GameObject messageBox;
    public Text message;
    public float timer;

    [Header("BlackOut Screen")]
    public Image blackoutImage;
    public Text blackoutText;

    [Header("Player Points")]
    public GameObject PointsUI;

    public void SetMensage(string p_mensage)
    {
        message.text = p_mensage;
        messageBox.gameObject.SetActive(true);
        Invoke("DesativeMensage", timer);
    }

    public void DesativeMensage()
    {
        messageBox.gameObject.SetActive(false);
    }

    public void CallBlackOutScreen()
    {
        blackoutImage.gameObject.SetActive(true);
        //tela de pause
    }

    private void Update()
    {
        //nada
    }
}
