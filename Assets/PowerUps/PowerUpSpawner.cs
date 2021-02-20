﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject FREEZE, ASSGROW, BLUESHELL, FAST, SLOW, HEADASS;

    public float spawnRate = 2f;

    float nextSpawn = 0.0f;

    int whatToSpawn;

    float randX;
    float randY;
    Vector2 whereToSpawn;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn){
            randX = Random.Range(20f, -20f);
            randY = Random.Range(20f, -20f);
            whereToSpawn = new Vector2(randX, randY);
            
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
