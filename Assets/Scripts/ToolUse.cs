using UnityEngine;

public class toolUse : MonoBehaviour
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
            //Debug.Log("Out of Reach");
        }
        //raycast with box, check if tag, else have a question mark appear
        //simple tags on breakable objects/ocean spots
    }
    void FixedUpdate()
    {
        //same thing with jump, but make it tool use with tag "breakable"
    }
    public bool IsFacingBreakable()
    {
        float lastPositionOffsetX = move.lastMovementDirection.x * move.breakableCheckOffsetX;
        float lastPositionOffsetY = move.lastMovementDirection.y * move.breakableCheckOffsetY;
        //Debug.Log(move.lastMovementDirection.x + " " + move.lastMovementDirection.y);
        Vector2 rayStart = new Vector2((transform.position.x + lastPositionOffsetX), (transform.position.y + lastPositionOffsetY));  

        //needs deubgging/box keeps hitting the player collider. wish unity had Debug.DrawBox but it don't
        RaycastHit2D hit = Physics2D.BoxCast(rayStart, move.boxSize, 0, move.lastMovementDirection, move.breakableCheckDistance);

        bool grounded = hit.collider != null;
        
        
        Debug.DrawRay(rayStart, move.lastMovementDirection*move.breakableCheckDistance, grounded ? Color.blue: Color.red);
        
        if(grounded)
        {
            //Debug.Log(hit.collider.tag);
            grounded = hit.collider.tag != "Player";
            if(grounded) {Debug.DrawRay(rayStart, move.lastMovementDirection*move.breakableCheckDistance,Color.red);}
        }
        else
        {
            Debug.DrawRay(rayStart, move.lastMovementDirection*move.breakableCheckDistance, Color.green);
        }
        
        return grounded;
    }
}
