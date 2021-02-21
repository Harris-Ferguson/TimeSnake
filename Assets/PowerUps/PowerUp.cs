using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    NONE, REWIND, FREEZE, ASSGROW, BLUESHELL, FAST, SLOW, DEAD, HEADASS
}

public class PowerUp : MonoBehaviour
{

    public Ability type;

    private SpriteRenderer sprite;

    public PowerUp(Ability type)
    {
        this.type = type;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
