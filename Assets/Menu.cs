using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        TimelineManager.Instance.enabled = true;
        Destroy(gameObject);
    }
    private bool starting = false;
    public void Update()
    {
        if (!starting&&Input.GetKeyDown(KeyCode.Space))
        {
            starting = true;
            GetComponent<Animator>().SetTrigger("Start");
        }
    }
}
