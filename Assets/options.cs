using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class options : MonoBehaviour
{
    // Start is called before the first frame update
    mainMenu menu;
    void Start()
    {
        menu = GameObject.FindFirstObjectByType<mainMenu>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
