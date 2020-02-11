using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Frames/Choice")]

public class Choice : Frame
{
    public Sprite characterImage;
    public string question;
    public string[] choices;
    public Frame[] frames;

    private void Awake()
    {
        if(choices!=null)
            if (choices.Length != frames.Length||choices.Length>3) throw new Exception("frames and choices not the same size or more than 3 choices");
    }

    public override void Load()
    {
        ChoiceManager.Instance.StartChoices(this);
    }
    public void End(int frameID)
    {
        ChoiceManager.Instance.EndChoice();
        CallEndFrame(frames[frameID]);
    }
}
