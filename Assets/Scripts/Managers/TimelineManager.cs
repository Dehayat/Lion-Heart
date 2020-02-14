using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public Frame InitialFrame;
    //[HideInInspector]
    public Frame currentFrame;
    public delegate Frame NextFrame();

    public static TimelineManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentFrame = InitialFrame;
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
            currentFrame = frame;
            frame.EndFrame += GoToNextFrame;
            frame.Load();
        }
        else
        {
            currentFrame = null;
        }
    }

    void GoToNextFrame(Frame frame)
    {
        LoadFrame(frame);
    }

}
