using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Frames/Scene")]
public class Scene : Frame
{

    public Frame nextFrame;
    public Sprite background;

    public bool fade = false;
    public float pauseBetweenFade = 0.5f;

    public override void Load() {
        SceneManager.Instance.LoadScene(this);
        //CallEndFrame(nextFrame);
    }

    internal void End()
    {
        CallEndFrame(nextFrame);
    }
}
