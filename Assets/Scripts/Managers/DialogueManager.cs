using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;
    public static DialogueManager Instance;

    public GameObject dialogueContainer;
    public Image imageHolderR;
    public Image imageHolderL;
    public Text nameHolder;
    public Text dialogueHolder;
    public KeyCode nextButton = KeyCode.Space;

    public float dialogueSpeed = 1;

    private delegate void State();
    private State CurrentState;
    private bool nextText = false;

    [SerializeField]
    private float charPos = 0;
    [SerializeField]
    private int StringPos = 0;


    private void Awake()
    {
        Instance = this;
        dialogueContainer.SetActive(false);
        dialogue = null;
    }

    void Update()
    {
        if (dialogue!=null)
        {
            GetInput();
            CurrentState?.Invoke();
        }
        else if (dialogueContainer.activeSelf)
        {
            dialogueContainer.SetActive(false);
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(nextButton))
        {
            if (CurrentState == WritingState)
            {
                charPos = dialogue.text[StringPos].Length;
            }
            else if(CurrentState == HoldState)
            {
                nextText = true;
            }
        }
    }

    public void StartDialogue(Dialogue d)
    {
        if(!dialogueContainer.activeSelf)
            dialogueContainer.SetActive(true);
        StringPos = 0;
        charPos = 0;
        imageHolderR.sprite = d.characterImageRight;
        SpriteManager.Instance.LoadCharacter(imageHolderR.gameObject, d.characterImageRight);
        SpriteManager.Instance.LoadCharacter(imageHolderL.gameObject, d.characterImageLeft);
        nameHolder.text = d.characterName;
        dialogueHolder.text = "";
        CurrentState = WritingState;
        dialogue = d;
    }

    internal void EndDialogue()
    {
        dialogue = null;
    }

    private void WritingState()
    {
        charPos += dialogueSpeed * Time.deltaTime;
        for (int i = dialogueHolder.text.Length; i < charPos&& i < dialogue.text[StringPos].Length; i++)
        {
            dialogueHolder.text += dialogue.text[StringPos][i];
        }

        if (charPos >= dialogue.text[StringPos].Length)
        {
            CurrentState = HoldState;
        }
    }
    private void HoldState()
    {
        if (nextText)
        {
            nextText = false;
            charPos = 0;
            StringPos++;
            CurrentState = WritingState;
            dialogueHolder.text = "";
            if (StringPos >= dialogue.text.Length)
            {
                dialogue.End();
            }
        }
    }

}
