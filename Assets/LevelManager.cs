using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Hard Coding in spawn values since the powerups do the same.
    // Obviously sloppy but I have no time
    public int numPlayers; 
    public Vector2 player1spawn = new Vector2(-4.07f, 4.09f);
    public Vector2 player2spawn = new Vector2(-4.07f, -4.02f);
    public Vector2 player3spawn = new Vector2(4.05f, 4.09f);
    public Vector2 player4spawn = new Vector2(4.05f, -4.02f);

    public bool inRound;

    private GameObject[] players;
    private Vector2[] spawns = new Vector2[4];



    // Start is called before the first frame update
    // this might be the worst code ive ever written...except maybe for my 100 level classes
    void Start()
    {
        players = Resources.LoadAll<GameObject>("PlayerPrefabs/Players");
        spawns[0] = player1spawn;
        spawns[1] = player2spawn;
        spawns[2] = player3spawn;
        spawns[3] = player4spawn;
        for(int i = 0; i < numPlayers; i++)
        {
            GameObject player = Instantiate(players[i], spawns[i], Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
