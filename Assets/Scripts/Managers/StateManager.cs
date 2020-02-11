using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public Dictionary<string, bool> states = new Dictionary<string, bool>();

    private void Awake()
    {
        Instance = this;
    }
}
