using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialougueTrigger : MonoBehaviour
{
    [SerializeField] private DialougueManager dialougueManager;

    public bool stopPlayerMovement = false;
    public bool useInteractionButton = false;

    private bool triggered;
    private SentenceManager sentences;

    private void Start()
    {
        sentences = gameObject.GetComponent<SentenceManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (useInteractionButton)
        {
            
        }
        else
        {
            if (other.CompareTag("Player") && !triggered)
            {
                dialougueManager.SetSentences(sentences);
                dialougueManager.SetPlayerMovement(!stopPlayerMovement);
                dialougueManager.TriggerStartDialougue();
                triggered = true;
            }
        }
        
    }
}
