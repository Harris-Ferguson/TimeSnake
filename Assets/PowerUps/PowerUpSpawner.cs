using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject FREEZE, ASSGROW, BLUESHELL, FAST, SLOW, HEADASS;

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
            //Random rnd = new Random();
            //randX = rnd.Next(0,7);
            //randY = rnd.Next(0,7);
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
            if(free == true){
                whatToSpawn = Random.Range(1,100);
                Debug.Log(whatToSpawn);

                if(whatToSpawn >0 && whatToSpawn <=5){
                    Instantiate(FREEZE, whereToSpawn, Quaternion.identity);

                }else if(whatToSpawn >5 && whatToSpawn <=20){
                    Instantiate(ASSGROW, whereToSpawn, Quaternion.identity);

                }else if(whatToSpawn >20 && whatToSpawn <=30){
                    Instantiate(BLUESHELL, whereToSpawn, Quaternion.identity);

                }else if(whatToSpawn >30 && whatToSpawn <=55){
                    Instantiate(FAST, whereToSpawn, Quaternion.identity);

                }else if(whatToSpawn >55 && whatToSpawn <=80){
                    Instantiate(SLOW, whereToSpawn, Quaternion.identity);

                }else if(whatToSpawn >80 && whatToSpawn <=100){
                    Instantiate(HEADASS, whereToSpawn, Quaternion.identity);
                }
            }
            

            nextSpawn = Time.time + spawnRate;
        }
    }
}
