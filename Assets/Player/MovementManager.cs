using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public GameObject tailPrefab;
    public float gridSize = 2;
    public float tailSizeOffset = 0.3f;


    public List<Transform> tail = new List<Transform>();
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
            createTail(savePos);

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

    public void AssGrowMove(Vector2 moveDir)
    {
        Vector2 savePos = transform.position;
        transform.Translate(moveDir * gridSize);
        if (ate)
        {
            ate = false;
        }
        if(tail.Count > 0)
        {
            createTail(savePos);
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
            createTail(pos.position);
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

    private void createTail(Vector2 pos)
    {
        GameObject newTail = Instantiate(tailPrefab, pos, Quaternion.identity);
        BoxCollider2D collider = newTail.GetComponent<BoxCollider2D>();
        collider.size = new Vector2(gridSize - tailSizeOffset, gridSize - tailSizeOffset);
        tail.Insert(0, newTail.transform);
    }

    public int snakeLength()
    {
        return tail.Count + 1;
    }
}
