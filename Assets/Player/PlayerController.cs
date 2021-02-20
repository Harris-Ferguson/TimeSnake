using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public float gridSize;
    public GameObject tailPrefab;

    private Vector2 moveDir;
    private List<Transform> tail = new List<Transform>();
    private List<PowerUp> powerups = new List<PowerUp>();
    private PowerUp lastPowerUp;
    private bool ate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MovePlayer", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        
    }

    private void HandleInput()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
            moveDir = Vector2.left;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            moveDir = Vector2.right;
        }
        else if(Input.GetAxis("Vertical") > 0)
        {
            moveDir = Vector2.up;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            moveDir = Vector2.down;
        }
    }

    public void MovePlayer()
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
            handlePowerups();
        }
        else if(tail.Count > 0)
        {
            Transform last = tail[tail.Count - 1];
            last.transform.position = savePos;
            tail.RemoveAt(tail.Count - 1);
            tail.Insert(0, last);
        }
    }

    private void handlePowerups()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collect power ups
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            ate = true;
            powerups.Add(collision.gameObject.AddComponent<PowerUp>());
            lastPowerUp = collision.gameObject.AddComponent<PowerUp>();
        }
    }

}
