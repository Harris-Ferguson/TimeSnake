using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Num_Players_Page : MonoBehaviour
{
    public bool is2;
    public bool is3;
    public bool is4;
    public bool isBack;

    void OnMouseUp(){
        if(is2)
        {
            Application.LoadLevel(1);
        }
        else if (is3)
        {
            Application.LoadLevel(1);
        }
        else if (is4)
        {
            Application.LoadLevel(1);
        }
        else if (isBack)
        {
            Application.LoadLevel(0);
        }
    } 
}
