using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public MovementManager mover;
    public int powerupsMax = 3;
    public int rewindFrames = 3;
    public int poweredUpFrames = 30;
    public float moveTicks = 0.5f;
    public string verticalInputName;
    public string horizontalInputName;
    public string inputFireName;
    public int wins = 0;


    private Vector2 moveDir;
    public List<PowerUp> powerups = new List<PowerUp>();
    public PowerUp currentPowerup;
    private List<List<Transform>> history = new List<List<Transform>>();
    public Ability state = Ability.NONE;
    private bool poweredUp;
    private int currentPoweredFrame;
    private int currentRewindFrame;

    // Start is called before the first frame update
    void Start()
    {
        state = Ability.NONE;
        currentPowerup = gameObject.AddComponent<PowerUp>();

        //find the sprite renderer
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

    public IEnumerator MovePlayer()
    {
        switch (state)
        {
            case Ability.NONE:
            {
                mover.NormalMove(moveDir);
                yield return new WaitForSeconds(moveTicks);
                break;
            }
            case Ability.REWIND:
            {
                mover.RenderSnake(history[0]);
                history.RemoveAt(0);
                currentRewindFrame--;
                if(currentRewindFrame <= 0)
                {
                    state = Ability.NONE;
                }
                yield return new WaitForSeconds(moveTicks);
                break;
            }
            /*
             * for fast and slow, we just need to reinvoke this coroutine with
            *  a different tick rate
            */
            case Ability.FAST:
            {
                mover.NormalMove(moveDir);
                yield return new WaitForSeconds(moveTicks / 2);
                break;
            }
            case Ability.SLOW:
            {
                mover.NormalMove(moveDir);
                yield return new WaitForSeconds(moveTicks * 2);
                break;
            }
            case Ability.FREEZE:
            {
                disableHitboxes();
                yield return new WaitForSeconds(moveTicks);
                break;
            }
            case Ability.ASSGROW:
            {
                mover.AssGrowMove(moveDir);
                yield return new WaitForSeconds(moveTicks);
                break;
            }
            case Ability.DEAD:
            {
                yield return new WaitForSeconds(moveTicks);
                break;
            }
        }
        checkPowerups();
        if(state != Ability.REWIND) addHisory();
        StartCoroutine(MovePlayer());
    }

    private void addHisory()
    { 
        history.Insert(0, mover.GetSnake());
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
                enableHitboxes();
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
        // die if we hit a non head part of any snake
        else if (collision.gameObject.CompareTag("Tail"))
        {
            die();
        }
        else if (collision.gameObject.CompareTag("Snake"))
        {
            int otherLength = collision.gameObject.GetComponent<MovementManager>().snakeLength();
            if(otherLength >= this.mover.snakeLength())
            {
                die();
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().die();
            }
        }
    }


    public void die()
    {
        //set our state to dead
        state = Ability.DEAD;

        //disable collision boxes
        disableHitboxes();

    }

    private void disableHitboxes()
    {
        BoxCollider2D[] hitboxes = GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D hitbox in hitboxes)
        {
            hitbox.enabled = false;
        }
        foreach(Transform tailPiece in mover.tail)
        {
            BoxCollider2D hitbox = tailPiece.GetComponent<BoxCollider2D>();
            hitbox.enabled = false;
        }
    }

    private void enableHitboxes()
    {
        BoxCollider2D[] hitboxes = GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D hitbox in hitboxes)
        {
            hitbox.enabled = true;
        }
        foreach(Transform tailPiece in mover.tail)
        {
            BoxCollider2D hitbox = tailPiece.GetComponent<BoxCollider2D>();
            hitbox.enabled = true;
        }
    }

    public void fullRewind()
    {
        state = Ability.REWIND;
        currentRewindFrame = history.Count;
    }

    private void HandleInput()
    {
    
        //Movement Input
        if(Input.GetAxis(horizontalInputName) < 0)
        {
            moveDir = Vector2.left;
        }
        else if(Input.GetAxis(horizontalInputName) > 0)
        {
            moveDir = Vector2.right;
        }
        else if(Input.GetAxis(verticalInputName) > 0)
        {
            moveDir = Vector2.up;
        }
        else if (Input.GetAxis(verticalInputName) < 0)
        {
            moveDir = Vector2.down;
        }

        //ability input
        if (Input.GetButtonDown(inputFireName))
        {
            if(powerups.Count < powerupsMax && powerups.Count > 0)
            {
                state = Ability.REWIND;
                currentRewindFrame = rewindFrames;
                powerups.Clear();
            }
            else if(powerups.Count <= 0)
            {
                return;
            }
            else
            {
                state = currentPowerup.type;
                poweredUp = true;
                currentPoweredFrame = 0;
                powerups.Clear();
            }
        }
    }
}


