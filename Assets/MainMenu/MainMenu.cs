using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public bool isStart;
    public bool isQuit;
    public bool isInstructions;

    void OnMouseUp(){
        if(isStart)
        {
            Application.LoadLevel(3);
        }
        else if (isQuit)
        {
            Application.Quit();
        }
        else if (isInstructions)
        {
            Application.LoadLevel(2);
        }
    } 
}
