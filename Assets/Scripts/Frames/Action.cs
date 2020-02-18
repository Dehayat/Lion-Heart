using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Frames/Action")]
public class Action : Frame
{
    public UnityEvent function;
    public Frame nextFrame;


    public override void Load()
    {
        Debug.Log("yo");
        function?.Invoke();
        CallEndFrame(nextFrame);
    }

}
