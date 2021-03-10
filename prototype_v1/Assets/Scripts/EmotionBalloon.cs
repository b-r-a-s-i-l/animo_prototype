using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotion { None, Exclamation, Interrogation }

public class EmotionBalloon : MonoBehaviour
{
    [Header("Emotion")]
    public Emotion value;

    [Header("Components")]
    public Sprite sprite;
    public RuntimeAnimatorController controller;

    private SpriteRenderer spriteRenderer;
    private Transform fatherReference;
    private GameObject balloon;

    public void CreateBalloon(Emotion value = Emotion.Exclamation)
    {
        balloon = new GameObject("EmotionBallon");

        fatherReference = gameObject.transform;
        fatherReference.position = gameObject.transform.position;

        balloon.transform.position = new Vector3(fatherReference.position.x, fatherReference.position.y + 1f);

        balloon.AddComponent<SpriteRenderer>();
        balloon.GetComponent<SpriteRenderer>().sprite = sprite;
        balloon.GetComponent<SpriteRenderer>().sortingLayerName = "UpperWalls";

        balloon.AddComponent<Animator>();
        balloon.GetComponent<Animator>().runtimeAnimatorController = controller;

        
        balloon.transform.SetParent(gameObject.transform);
        balloon.gameObject.SetActive(false);
   }

    public void SetEmotion(Emotion value)
    {
        int type = (int)value;
        balloon.GetComponent<Animator>().SetInteger("Type", type);
    }

    public void OpenAnimate()
    {
        balloon.gameObject.SetActive(true);
        balloon.GetComponent<Animator>().SetBool("Enter", true);
        SetEmotion(value);
    }

    public void CloseAnimate()
    {
        balloon.GetComponent<Animator>().SetBool("Enter", false);
        Invoke("DesactiveBalloon", 0.2f);
    }

    public void DestroyBalloon()
    {
        balloon.gameObject.SetActive(false);
    }
}
