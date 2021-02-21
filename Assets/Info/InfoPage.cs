using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPage : MonoBehaviour
{
    public bool isBack;

    void OnMouseUp(){
        if(isBack)
        {
            Application.LoadLevel(0);
        }
    } 
}
