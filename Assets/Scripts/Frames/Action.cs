using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Frames/Action")]
public class Action : Frame
{
    //public UnityEvent function;
    public Frame nextFrame;

    public bool condition;
    public string id;

    public override void Load()
    {
        //function.Invoke();
        if (StateManager.Instance.states.ContainsKey(id))
        {
            StateManager.Instance.states[id] = condition;
        }
        else
        {
            StateManager.Instance.states.Add(id, condition);
        }
        CallEndFrame(nextFrame);
    }

}
