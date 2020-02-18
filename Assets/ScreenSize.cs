using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var s = GetComponent<SpriteRenderer>().sprite;
        print(s.pivot/s.pixelsPerUnit); 
    }
}
