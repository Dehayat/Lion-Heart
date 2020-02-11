using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameObject : MonoBehaviour
{

    public MiniGame myFrame;

    public void FinishGame(bool win)
    {
        myFrame.FinishGame(win);
    }
}
