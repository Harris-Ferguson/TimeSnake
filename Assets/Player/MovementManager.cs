using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public GameObject tailPrefab;
    public float gridSize = 2;


    private List<Transform> tail = new List<Transform>();
    private bool ate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalMove(Vector2 moveDir)
    {
        Vector2 savePos = transform.position;
        transform.Translate(moveDir * gridSize);
        // if we have a tail move the last element to the front
        if (ate)
        {
            //first add a new body part to the snake
            GameObject newPart = Instantiate(tailPrefab, savePos, Quaternion.identity);
            tail.Insert(0, newPart.transform);

            // reset the ate flag
            ate = false;
        }
        else if(tail.Count > 0)
        {
            Transform last = tail[tail.Count - 1];
            last.transform.position = savePos;
            tail.RemoveAt(tail.Count - 1);
            tail.Insert(0, last);
        }
    }

    // Deletes our current position and renders the given snake as a list of positions
    public void RenderSnake(List<Transform> snake)
    {
        // delete the current snake
        foreach(Transform pos in tail)
        {
            Destroy(pos.gameObject);
        }
        tail.Clear();
        //move our head
        transform.SetPositionAndRotation(snake[0].position, Quaternion.identity);
        snake.RemoveAt(0);
        foreach(Transform pos in snake)
        {
            GameObject newPart = Instantiate(tailPrefab, pos.position, Quaternion.identity);
            tail.Insert(0, newPart.transform);
        }
    }

    public void SetAte()
    {
        ate = true;
    }

    public List<Transform> GetSnake()
    {
        List<Transform> snake = new List<Transform>();
        GameObject saveHead = new GameObject();
        saveHead.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        snake.Insert(0, saveHead.transform);
        foreach(Transform pos in tail)
        {
            GameObject newObj = new GameObject();
            newObj.transform.SetPositionAndRotation(pos.transform.position, Quaternion.identity);
            snake.Add(newObj.transform);
        }
        return snake;
    }
}
