using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChoiceManager : MonoBehaviour
{
    public Choice choice;
    public static ChoiceManager Instance;

    public GameObject choiceContainer;
    public Image imageHolder;
    public Text questionHolder;
    public GameObject[] choicesContainer;
    public KeyCode nextButton = KeyCode.Space;

    public float dialogueSpeed = 1;

    private delegate void State();
    private State CurrentState;

    [SerializeField]
    private float charPos = 0;

    private void Awake()
    {
        Instance = this;
        choiceContainer.SetActive(false);
        choice = null;
    }
    void Update()
    {
        if (choice != null)
        {
            GetInput();
            CurrentState?.Invoke();
        }
        else if (choiceContainer.activeSelf)
        {
            choiceContainer.SetActive(false);
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(nextButton))
        {
            if (CurrentState == WritingState)
            {
                charPos = choice.question.Length;
            }
        }
    }

    internal void EndChoice()
    {
        if (choiceContainer.GetComponent<Animator>() != null)
        {
            choiceContainer.GetComponent<Animator>().SetTrigger("Hide");
        }
        choice = null;
    }

    public void StartChoices(Choice c)
    {
        if (!choiceContainer.activeSelf)
            choiceContainer.SetActive(true);
        charPos = 0;
        imageHolder.sprite = c.characterImage;
        questionHolder.text = "";
        foreach (var item in choicesContainer)
        {
            item.SetActive(false);
        }
        CurrentState = WritingState;
        choice = c;
    }
    private void WritingState()
    {
        //charPos += dialogueSpeed * Time.deltaTime;
        //if(questionHolder.text.Length < charPos && questionHolder.text.Length < choice.question.Length)
        //{
        //    //SoundManager.Instance.Stop();
        //    //SoundManager.Instance.Play();
        //}
        //for (int i = questionHolder.text.Length; i < charPos && i < choice.question.Length; i++)
        //{
        //    questionHolder.text += choice.question[i];
        //}

        //if (charPos >= choice.question.Length)
        //{
            ShowChoices();
            CurrentState = HoldState;
        //}
    }

    private void ShowChoices()
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            GameObject g = choicesContainer[i];
            g.GetComponentInChildren<Text>().text = choice.choices[i];
            Button b = g.GetComponent<Button>();
            int x = i;
            b.onClick.AddListener(delegate {choice.End(x); });
            g.SetActive(true);
        }
    }

    private void HoldState()
    {

    }
}
