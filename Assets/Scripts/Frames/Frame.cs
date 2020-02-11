using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : ScriptableObject
{

    public delegate void Done(Frame nextFrame);
    public event Done EndFrame;

    public virtual void Load() { }
    public virtual void Exit() { }
    protected void CallEndFrame(Frame nextFrame)
    {
        EndFrame(nextFrame);
    }
}
