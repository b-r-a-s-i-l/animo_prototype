using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpriteBubble { Thinking, Talking }

public class SentenceManager : MonoBehaviour
{
    [System.Serializable]
    struct SentenceContainer
    {
        public SpriteBubble sprite;
        public bool autoContinue;
        [TextArea] public string text;
    }

    [Header("Dialougue Sentences")]
    [SerializeField] private SentenceContainer[] containers;

    public string GetText(int index)
    {
        return containers[index].text;
    }

    public int Length()
    {
        return containers.Length;
    }
}
