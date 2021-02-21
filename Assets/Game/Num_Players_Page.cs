using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Num_Players_Page : MonoBehaviour
{
    public bool is2;
    public bool is3;
    public bool is4;
    public bool isBack;

    void OnMouseUp(){
        if(is2)
        {
            LevelManager.numPlayers = 2;
        }
        else if (is3)
        {
            LevelManager.numPlayers = 3;
        }
        else if (is4)
        {
            LevelManager.numPlayers = 4;
        }
        else if (isBack)
        {
            SceneManager.LoadScene("MainMenu");
        }
        SceneManager.LoadScene("Game");
    } 
}
