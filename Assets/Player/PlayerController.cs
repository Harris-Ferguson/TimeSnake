using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public float gridSize;

    private Vector2 moveDir;



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
        transform.Translate(moveDir * gridSize);
    }


}
