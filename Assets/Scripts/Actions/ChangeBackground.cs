using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public void _ChangeBackground(Sprite sprite)
    {
        SpriteManager.Instance.ChangeBackground(sprite);
    }
}
