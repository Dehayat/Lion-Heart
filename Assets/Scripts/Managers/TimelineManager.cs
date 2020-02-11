using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public Frame currentFrame;
    public delegate Frame NextFrame();


    void Start()
    {
        LoadFrame(currentFrame);
    }

    private void LoadFrame(Frame frame)
    {
        if (currentFrame)
        {
            currentFrame.EndFrame -= GoToNextFrame;
            currentFrame.Exit();
        }
        if (frame != null)
        {
            frame.EndFrame += GoToNextFrame;
            frame.Load();
            currentFrame = frame;
        }
    }

    void GoToNextFrame(Frame frame)
    {
        LoadFrame(frame);
    }

}
