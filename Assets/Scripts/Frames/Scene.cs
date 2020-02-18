using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Frames/Scene")]
public class Scene : Frame
{

    public Frame nextFrame;
    public Sprite background;

    public override void Load() {
        //SpriteManager.Instance.LoadScene(this);
        CallEndFrame(nextFrame);
    }
}
