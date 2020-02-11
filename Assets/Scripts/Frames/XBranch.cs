using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Frames/Explicit Branch")]

public class XBranch : Frame
{
    public Frame trueFrame;
    public Frame falseFrame;
    public bool condition;
    public string id;

    public override void Load()
    {
        bool ret = StateManager.Instance.states.TryGetValue(id, out ret)?ret:condition;
        if (ret)
        {
            CallEndFrame(trueFrame);
        }
        else
        {
            CallEndFrame(falseFrame);
        }
    }
}
