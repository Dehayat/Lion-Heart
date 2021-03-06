﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Frames/Dialogue")]
public class Dialogue : Frame
{
    public Frame nextFrame;
    public Sprite characterImage;
    public string characterName;
    public string[] text;
    public Vector3 speechBubblePivot = new Vector3(-2.5f,1.5f,0f);

    public override void Load()
    {
        DialogueManager.Instance.StartDialogue(this);
    }

    internal void End()
    {
        DialogueManager.Instance.EndDialogue();
        CallEndFrame(nextFrame);
    }
}
