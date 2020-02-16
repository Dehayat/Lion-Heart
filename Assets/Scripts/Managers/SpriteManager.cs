using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public GameObject MainUI;
    public static SpriteManager Instance;
    public Image backgroundImage;

    private void Awake()
    {
        Instance = this;
    }

    internal void ChangeBackground(Sprite sprite)
    {
        backgroundImage.sprite = sprite;
    }

    public void LoadCharacter(GameObject imageCon,Sprite image,bool Fast=true)
    {

        Image im = imageCon.GetComponent<Image>();
        imageCon.SetActive(false);
        if (im != null)
        {
            if (image != null)
            {
                imageCon.SetActive(true);
                im.sprite = image;
            }
        }
    }

}
