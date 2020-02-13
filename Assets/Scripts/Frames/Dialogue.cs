using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Frames/Dialogue")]
public class Dialogue : Frame
{
    public Frame nextFrame;
    public Sprite characterImageRight;
    public Sprite characterImageLeft;
    public string characterName;
    public string[] text;

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
