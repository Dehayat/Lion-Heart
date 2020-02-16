using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCondition : MonoBehaviour
{
    public void _StateTrue(string id)
    {
        if (StateManager.Instance.states.ContainsKey(id))
        {
            StateManager.Instance.states[id] = true;
        }
        else
        {
            StateManager.Instance.states.Add(id, true);
        }
    }
    public void _StateFalse(string id)
    {
        if (StateManager.Instance.states.ContainsKey(id))
        {
            StateManager.Instance.states[id] = false;
        }
        else
        {
            StateManager.Instance.states.Add(id, false);
        }
    }

}
