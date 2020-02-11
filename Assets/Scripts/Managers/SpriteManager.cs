using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public GameObject MainUI;
    public static SpriteManager Instance;
    private void Awake()
    {
        Instance = this;
    }
}
