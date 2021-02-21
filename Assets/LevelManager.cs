using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Hard Coding in spawn values since the powerups do the same.
    // Obviously sloppy but I have no time
    public Vector2 player1spawn = new Vector2(-4.07f, 4.09f);
    public Vector2 player2spawn = new Vector2(-4.07f, -4.02f);
    public Vector2 player3spawn = new Vector2(4.05f, 4.09f);
    public Vector2 player4spawn = new Vector2(4.05f, -4.02f);

    private GameObject[] players;
    private Vector2[] spawns = new Vector2[4];
    public List<GameObject> activePlayers = new List<GameObject>();
    public static int playedRounds = 0;
    public static int numPlayers = 2;
    public static int totalRounds = 3;

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
            activePlayers.Add(player);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkOver())
        {
            if(playedRounds < totalRounds)
            {
                playedRounds++;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
            else
            {
                //Load game over screen here
            }
        }
    }

    private bool checkOver()
    {
        foreach(GameObject player in activePlayers)
        {
            PlayerController contr = player.GetComponent<PlayerController>();
            if(contr.state != Ability.DEAD)
            {
                return false;
            }
        }
        return true;
    }
}
