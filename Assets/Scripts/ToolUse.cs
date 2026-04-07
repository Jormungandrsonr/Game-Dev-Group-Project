using UnityEngine;

public class toolUse : BasicMovement
{
    Rigidbody2D rb2d;
    BasicMovement move;
    Collider2D collide;


    
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        move = GetComponent<BasicMovement>();
        collide = GetComponent<Collider2D>();
    }
    void Update()
    {
        bool breakableInReach = IsFacingBreakable();
        if(breakableInReach)
        {
            Debug.Log("In reach.");
        } 
        else
        {
            Debug.Log("Out of Reach");
        }
        //raycast with box, check if tag, else have a question mark appear
        //simple tags on breakable objects/ocean spots
    }
    void FixedUpdate()
    {
        //same thing with jump, but make it tool use with tag "breakable"
    }
}
