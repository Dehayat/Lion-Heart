using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public SpriteRenderer Temp;

    public static SceneManager Instance;

    private Scene scene;

    private Animator anim;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    internal void LoadScene(Scene s)
    {
        //if there is no background fade is mandatory
        GetComponent<SpriteMask>().sprite = s.background;
        if (scene == null)
        {
            scene = s;
            FadeInAnim();
        }
        else if (s.fade)
        {
            scene = s;
            anim.SetTrigger("FadeOut");
        }
        else
        {
            Temp.sprite = scene.background;
            scene = s;
            anim.SetTrigger("Flip");
        }
    }
    public void FadeInAnim()
    {
        GetComponent<SpriteRenderer>().sprite = scene.background;
        StartCoroutine(StartFadeInAfter());
    }

    IEnumerator StartFadeInAfter()
    {
        yield return new WaitForSeconds(scene.pauseBetweenFade);
        anim.SetTrigger("FadeIn");
    }

    public void FadeInAnimDone()
    {
        scene.End();
    }
    public void FadeOutAnimDone()
    {
        FadeInAnim();
    }

    public void SetNewBG()
    {
        GetComponent<SpriteRenderer>().sprite = scene.background;
    }
}
