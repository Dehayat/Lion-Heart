using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;

    public GameObject MainUI;

    public GameObject BackgroundContainer;
    public Image backgroundImage, oldBackgroundImage;



    private Scene BG;


    private void AnimateBackground(string id, bool play)
    {
        //Animator anim = BackgroundContainer.GetComponent<Animator>();
        //if (anim != null)
        //{
        //    anim.SetBool(id, play);
        //}
    }

    public void LoadScene(Scene scene)
    {
        if (backgroundImage.sprite == null)
        {
            AnimateBackground("FadeIn", true);
        }
        else {
            AnimateBackground("NextPage", true);
        }
        BG = scene;
    }

    private void Awake()
    {
        Instance = this;
    }
    internal void ChangeBackground(Sprite sprite=null)
    {
        if (sprite == null)
        {
            sprite = BG.background;
        }
        backgroundImage.sprite = sprite;
    }
    internal void ChangeOldBackground(Sprite sprite = null)
    {
        if (sprite == null)
        {
            sprite = BG.background;
        }
        oldBackgroundImage.sprite = sprite;
        AnimateBackground("NextPage", false);
        AnimateBackground("FadeIn", false);
        //BG.End();
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
