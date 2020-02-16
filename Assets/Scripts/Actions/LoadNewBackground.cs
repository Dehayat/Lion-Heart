using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadNewBackground : MonoBehaviour
{
    public void _LoadNewBackground()
    {
        SpriteManager.Instance.ChangeBackground();
    }
    public void _LoadOldBackground()
    {
        SpriteManager.Instance.ChangeOldBackground();
    }
}
