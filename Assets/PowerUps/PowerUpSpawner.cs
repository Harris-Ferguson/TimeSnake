using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnRate = 2f;

    float nextSpawn = 0.0f;

    int whatToSpawn;

    int randX;
    int randY;

    Random Xgen;
    Random Ygen;

    Vector2 whereToSpawn;

    double[] PossibleX = {-4.07, -2.9, -1.76, -0.62, 0.55, 1.7, 2.89, 4.05};
    double[] PossibleY = {4.09, 2.93, 1.73, 0.6, -0.6, -1.76, -2.88, -4.02};

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn){
            randX = Random.Range(0,8);
            randY = Random.Range(0,8);

            whereToSpawn = new Vector2((float)PossibleX[randX], (float)PossibleY[randY]);
            
            GameObject[] list = GameObject.FindGameObjectsWithTag("PowerUp");
            bool free = true;
            foreach (GameObject power in list){
                if(power.transform.position.x == whereToSpawn.x && power.transform.position.y == whereToSpawn.y){
                    free = false;
                }
            }

            list = GameObject.FindGameObjectsWithTag("Snake");
            foreach (GameObject part in list){
                if(((part.transform.position.x - whereToSpawn.x <= .40 ) && (part.transform.position.x- whereToSpawn.x >= -.40)) && ((part.transform.position.y- whereToSpawn.y <= .40 ) && (part.transform.position.y - whereToSpawn.y >= - .40))){
                    free = false;
                }
            }

            list = GameObject.FindGameObjectsWithTag("Tail");
            foreach (GameObject part in list){
                if((part.transform.position.x- whereToSpawn.x <= .40 && part.transform.position.x- whereToSpawn.x >= - .40) && (part.transform.position.y- whereToSpawn.y <= .40 && part.transform.position.y - whereToSpawn.y >= - .40)){
                    free = false;
                }
            }


            if(free == true){
                whatToSpawn = Random.Range(1,100);

                if(whatToSpawn >0 && whatToSpawn <=5)
                {
                    spawnPowerUp(Ability.FREEZE);
                }


                else if(whatToSpawn >5 && whatToSpawn <=20)
                {
                    spawnPowerUp(Ability.ASSGROW);

                }
                else if(whatToSpawn >20 && whatToSpawn <=30)
                {
                    spawnPowerUp(Ability.BLUESHELL);

                }
                else if(whatToSpawn >30 && whatToSpawn <=55)
                {
                    spawnPowerUp(Ability.FAST);

                }
                else if(whatToSpawn >55 && whatToSpawn <=80)
                {
                    spawnPowerUp(Ability.SLOW);

                }
                else if(whatToSpawn >80 && whatToSpawn <=100)
                    {
                    spawnPowerUp(Ability.HEADASS);
                }
            }
            nextSpawn = Time.time + spawnRate;
        }
    }

    public void spawnPowerUp(Ability type)
    {
        GameObject newObj = Instantiate(powerUpPrefab, whereToSpawn, Quaternion.identity);
        PowerUp power = newObj.GetComponent<PowerUp>();
        SpriteRenderer sprite = newObj.GetComponent<SpriteRenderer>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("misc assets");
        switch (type)
        {
            case Ability.FREEZE:
                sprite.sprite = sprites[13];
                break;
            case Ability.HEADASS:
                sprite.sprite = sprites[18];
                break;
            case Ability.BLUESHELL:
                sprite.sprite = sprites[15];
                break;
            case Ability.FAST:
                sprite.sprite = sprites[16];
                break;
            case Ability.SLOW:
                sprite.sprite = sprites[17];
                break;
            case Ability.ASSGROW:
                sprite.sprite = sprites[14];
                break;
        }
        power.type = type;
    }
}
