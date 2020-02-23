using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;
    public static DialogueManager Instance;

    //public GameObject dialogueContainer;
    //public Image imageHolderR;
    //public Image imageHolderL;
    public SpriteRenderer characterImage;
    public GameObject SpeechBubble;
    //public Text nameHolder;
    public TextMeshProUGUI dialogueText;
    public KeyCode nextButton = KeyCode.Space;

    //public float dialogueSpeed = 1;

    private delegate void State();
    private State CurrentState;
    private bool nextText = false;

    //Animatoion vars
    private Animator anim;
    [SerializeField]
    bool animating = false;
    [SerializeField]
    private bool frozen = false;

    //private float charPos = 0;
    private int StringPos = 0;


    private void Awake()
    {
        Instance = this;
        //dialogueContainer.SetActive(false);
        dialogue = null;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (dialogue!=null)
        {
            GetInput();
            CurrentState?.Invoke();
        }
        //else if (dialogueContainer.activeSelf)
        //{
        //    dialogueContainer.SetActive(false);
        //}
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(nextButton))
        {
            //if (CurrentState == WritingState)
            //{
            //    charPos = dialogue.text[StringPos].Length;
            //}
            if(CurrentState == HoldState)
            {
                nextText = true;
            }
        }
    }

    public void StartDialogue(Dialogue d)
    {
        dialogue = d;
        //if(!dialogueContainer.activeSelf)
        //    dialogueContainer.SetActive(true);
        StringPos = 0;
        //charPos = 0;
        //imageHolderR.sprite = d.characterImageRight;
        //SpriteManager.Instance.LoadCharacter(imageHolderR.gameObject, d.characterImageRight);
        //SpriteManager.Instance.LoadCharacter(imageHolderL.gameObject, d.characterImageLeft);
        //nameHolder.text = d.characterName;
        dialogueText.text = "";
        CurrentState = LoadState;
    }

    internal void EndDialogue()
    {
        dialogue = null;
    }

    private void LoadState()
    {
        if (!animating)
        {
            if (dialogue.fastTransition)
            {
                anim.SetTrigger("LoadFast");
            }
            else
            {
                if (frozen)
                {
                    anim.SetTrigger("Unfreeze");
                    frozen = false;
                }
                else
                {
                    characterImage.sprite = dialogue.characterImage;
                }
                anim.SetTrigger("Load");
            }
            animating = true;
        }
    }

    private void WritingState()
    {
        if (!animating)
        {
            RecalcBubbleSize();
            SpeechBubble.transform.position = dialogue.speechBubblePivot;
            anim.SetTrigger("ShowDialogue");
            animating = true;
            dialogueText.richText = true; 
            dialogueText.text = dialogue.text[StringPos];
        }
    }
    private void HoldState()
    {
        if (nextText)
        {
            nextText = false;
            //charPos = 0;
            StringPos++;
            CurrentState = HidingState;
        }
    }

    private void HidingState()
    {
        if (!animating)
        {
            anim.SetTrigger("HideDialogue");
            animating = true;
        }
    }

    private void UnLoadState()
    {
        if (!animating)
        {
            if (dialogue.keepAlive)
            {
                anim.SetTrigger("Freeze");
                frozen = true;
                UnLoadAnimDone();
            }
            else
            {
                anim.SetTrigger("UnLoad");
                animating = true;
            }
        }
    }

    //Helper Functions

    private void RecalcBubbleSize()
    {
        var bubble = SpeechBubble.GetComponent<RectTransform>().rect;
        float t = (dialogue.text[StringPos].Length+20) / 80f;
        var d = Vector2.Lerp(new Vector2(2, 1.4f), new Vector2(4, 2.8f), t);
        SpeechBubble.GetComponent<RectTransform>().sizeDelta = d;
    }

    //Animator Events

    public void LoadAnimDone()
    {
        animating = false;
        if (frozen) frozen = false;
        if (StringPos < dialogue.text.Length)
        {
            CurrentState = WritingState;
        }
        else
        {
            CurrentState = UnLoadState;
        }
    }

    public void ShowAnimDone()
    {
        animating = false;
        CurrentState = HoldState;
    }
    public void HideAnimDone()
    {
        animating = false;
        if (StringPos < dialogue.text.Length)
        {
            CurrentState = WritingState;
        }
        else
        {
            CurrentState = UnLoadState;
        }
    }
    public void UnLoadAnimDone()
    {
        animating = false;
        CurrentState = null;
        dialogue.End();
    }
    public void UnfreezeAnimDone()
    {
        characterImage.sprite = dialogue.characterImage;
    }
}
