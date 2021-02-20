using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public MovementManager mover;
    private Vector2 moveDir;
    private List<PowerUp> powerups = new List<PowerUp>();
    private PowerUp lastPowerUp;

    private List<List<Transform>> history = new List<List<Transform>>();

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

    public void MovePlayer()
    {
        mover.NormalMove(moveDir);



        history.Add(mover.GetSnake());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collect power ups
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            mover.SetAte();
            powerups.Add(collision.gameObject.AddComponent<PowerUp>());
            lastPowerUp = collision.gameObject.AddComponent<PowerUp>();
        }
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
}
