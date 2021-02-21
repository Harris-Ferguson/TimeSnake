using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LevelManager level;
    public GameObject ui;
    public Sprite[] miscSprites;


    // Start is called before the first frame update
    void Start()
    {
        miscSprites = Resources.LoadAll<Sprite>("misc assets");
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1 ; i <= LevelManager.numPlayers; i++)
        {
            PlayerController player = level.activePlayers[i-1].GetComponent<PlayerController>();
            GameObject playerui = ui.transform.Find("player" + i.ToString()).gameObject;


            //set the power up indicator
            GameObject powerUpIndicator = playerui.transform.Find("powerup").gameObject;
            SpriteRenderer pUsprite = powerUpIndicator.GetComponent<SpriteRenderer>();
            switch (player.currentPowerup.type)
            {
                case Ability.FREEZE:
                    pUsprite.sprite = miscSprites[22];
                    break;
                case Ability.FAST:
                    pUsprite.sprite = miscSprites[28];
                    break;
                case Ability.SLOW:
                    pUsprite.sprite = miscSprites[29];
                    break;
                case Ability.ASSGROW:
                    pUsprite.sprite = miscSprites[23];
                    break;
                case Ability.BLUESHELL:
                    pUsprite.sprite = miscSprites[24];
                    break;
                case Ability.NONE:
                    pUsprite.sprite = miscSprites[21];
                    break;
            }
            if(player.powerups.Count < player.powerupsMax)
            {
                pUsprite.color = new Color(108, 108, 108);
            }
            else
            {
                pUsprite.color = new Color(250, 250, 250);
            }
            // set the crowns
            //SpriteRenderer crown1 = playerui.transform.Find("crown1").gameObject.GetComponent<SpriteRenderer>();
            //SpriteRenderer crown2 = playerui.transform.Find("crown2").gameObject.GetComponent<SpriteRenderer>();
            //SpriteRenderer crown3 = playerui.transform.Find("crown3").gameObject.GetComponent<SpriteRenderer>();
            //crown1.sprite = miscSprites[7];
            //crown2.sprite = miscSprites[7];
            //crown3.sprite = miscSprites[7];
            //if(player.wins >= 1)
            //{
            //    crown1.sprite = miscSprites[8];
            //}
            //if(player.wins >= 2)
            //{
            //    crown2.sprite = miscSprites[8];
            //}
            //if(player.wins >= 3)
            //{
            //    crown2.sprite = miscSprites[8];
            //}

        }
    }
}
