using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Frames/Mini Game")]

public class MiniGame : Frame
{
    public Frame trueFrame;
    public Frame falseFrame;
    public GameObject game;

    private GameObject currentGame;

    public override void Load()
    {
        SpriteManager.Instance.MainUI.SetActive(false);
        currentGame = Instantiate(game);
        currentGame.GetComponent<MiniGameObject>().myFrame = this;
    }
    public void FinishGame(bool win)
    {
        Destroy(currentGame);
        SpriteManager.Instance.MainUI.SetActive(true);
        currentGame = null;
        if (win)
        {
            CallEndFrame(trueFrame);
        }
        else
        {
            CallEndFrame(falseFrame);
        }
    }
}
