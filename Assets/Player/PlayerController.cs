using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public MovementManager mover;
    public int powerupsMax;
    public int rewindFrames = 3;
    public int poweredUpFrames = 30;
    public float moveTicks = 0.5f;


    private Vector2 moveDir;
    private List<PowerUp> powerups = new List<PowerUp>();
    private PowerUp currentPowerup;
    private List<List<Transform>> history = new List<List<Transform>>();
    private Ability state = Ability.NONE;
    private bool poweredUp;
    private int currentPoweredFrame;

    // Start is called before the first frame update
    void Start()
    {
        state = Ability.NONE;
        currentPowerup = new PowerUp(Ability.NONE);
        StartCoroutine(MovePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator rewind(int steps, float stepTime)
    {
        while(steps > 0 && history.Count > 0)
        {
            mover.RenderSnake(history[0]);
            history.RemoveAt(0);
            steps--;
            yield return new WaitForSeconds(stepTime);
        }
        state = Ability.NONE;
    }

    public IEnumerator MovePlayer()
    { 
        switch (state)
        {
            case Ability.NONE:
            {
                mover.NormalMove(moveDir);
                addHisory();
                yield return new WaitForSeconds(moveTicks);
                break;
            }
            case Ability.REWIND:
            {
                yield return new WaitForSeconds(moveTicks);
                break;
            }
            case Ability.FAST:
            {
                mover.NormalMove(moveDir);
                addHisory();
                yield return new WaitForSeconds(moveTicks / 2);
                break;
            }
        }
        StartCoroutine(MovePlayer());
    }

    private void addHisory()
    {
        // as long as we aren't rewining keep track of our movement history
        if (state != Ability.REWIND)
        {
            history.Insert(0, mover.GetSnake());
        }
    }

    private void checkPowerups()
    {
        //check if we should be powered up next step
        if (poweredUp)
        {
            currentPoweredFrame++;
            if(currentPoweredFrame >= poweredUpFrames)
            {
                poweredUp = false;
                state = Ability.NONE;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collect power ups
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            PowerUp powerup = collision.gameObject.GetComponent<PowerUp>();
            mover.SetAte();
            if(currentPowerup.type == Ability.NONE)
            {
                currentPowerup = new PowerUp(powerup.type);
                powerups.Add(new PowerUp(powerup.type));
            }
            else if(powerup.type == currentPowerup.type && powerups.Count < powerupsMax)
            {
                powerups.Add(new PowerUp(powerup.type));
            }
            else
            {
                powerups.Clear();
                powerups.Add(new PowerUp(powerup.type));
            }
            Destroy(collision.gameObject);
        }
    }

    private void HandleInput()
    {
    
        //Movement Input
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

        //ability input
        if (Input.GetButtonDown("Fire1"))
        {
            if(powerups.Count < powerupsMax && powerups.Count > 0)
            {
                state = Ability.REWIND;
                StartCoroutine(rewind(rewindFrames, 0.2f));
            }
            if(powerups.Count <= 0)
            {
                return;
            }
            else
            {
                state = currentPowerup.type;
                poweredUp = true;
                currentPoweredFrame = 0;
            }
        }
    }
}
