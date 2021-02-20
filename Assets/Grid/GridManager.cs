using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    private int rows = 11;
    private int cols = 11;
    private float tileSize = 1;
    public GameObject Grid;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    
    }

    private void GenerateGrid(){
        GameObject referenceTile = (GameObject)Instantiate(Grid);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject tile  = (GameObject)Instantiate(referenceTile, transform);

                float posX = j*tileSize;
                float posY = i* -tileSize;

                tile.transform.position = new Vector2(posX, posY);
            }

            
        }

        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize/2, gridH / 2 - tileSize / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
