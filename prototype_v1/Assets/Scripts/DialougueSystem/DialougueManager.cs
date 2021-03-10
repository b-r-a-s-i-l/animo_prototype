using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougueManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private bool playerSpeakingFirst;
    [SerializeField] private TextMeshProUGUI dialougueText;
    [SerializeField] private GameObject continueBtn;
    [SerializeField] private Animator animatorController;
    [SerializeField] private AudioSource audioSource;

    private SentenceManager sentences;

    private int index;
    private bool dialougueStarted;
    private float animatorDelay = 0.6f;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (continueBtn.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TriggerContinuePlayerDialougue();
            }
        }
    }

    public void TriggerStartDialougue()
    {
        StartCoroutine(StartDialougue());
    }

    public IEnumerator StartDialougue()
    {
        dialougueStarted = true;

        if (playerSpeakingFirst)
        {
            animatorController.SetTrigger("Open");
            yield return new WaitForSeconds(animatorDelay);
            StartCoroutine(TypeDialougue());
        }
        else
        {
            //chamar conversa do outro objeto
        }
    }

    private IEnumerator TypeDialougue()
    {
        foreach (char letter in sentences.GetText(index).ToCharArray())
        {
            dialougueText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        continueBtn.SetActive(true);
    }


    private IEnumerator ContinueDialougue()
    {
        /*
        --------- FECHAR E ABRIR BALLON---------------
        dialougueText.text = string.Empty;
        animatorController.SetTrigger("Close");
        yield return new WaitForSeconds(animatorDelay);
        dialougueText.text = string.Empty;
        animatorController.SetTrigger("Open");
        yield return new WaitForSeconds(animatorDelay);
        -----------------------------------------------
        */

        dialougueText.text = string.Empty;
        yield return new WaitForSeconds(animatorDelay);

        if (dialougueStarted)
            index++;
        else
            dialougueStarted = true;

        StartCoroutine(TypeDialougue());
    }

    public void TriggerContinuePlayerDialougue()
    {
        audioSource.Play();

        continueBtn.SetActive(false);

        if (index >= sentences.Length() - 1)
        {
            dialougueText.text = string.Empty;
            animatorController.SetTrigger("Close");
            playerMovement.TogglePlayerMovement(true);
        }
        else StartCoroutine(ContinueDialougue());
    }

    //SETs

    public void SetPlayerMovement(bool value)
    {
        playerMovement.TogglePlayerMovement(value);
    }

    public void SetSentences(SentenceManager objectSentences)
    {
        sentences = objectSentences;
    }

}
