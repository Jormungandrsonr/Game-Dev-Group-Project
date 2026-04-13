using UnityEngine;

public class ToolUse : MonoBehaviour
{
    Rigidbody2D rb2d;
    BasicMovement move;
    Collider2D collide;
    bool inReach = false;


    
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
            inReach = true;
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
        if(inReach)
        {
            Debug.Log("");
            inReach = false;
        }
        //same thing with jump, but make it tool use with tag "breakable"
    }
    public bool IsFacingBreakable()
    {
        float tempMoveX = move.lastMovementDirection.x;
        float tempMoveY = move.lastMovementDirection.y;


        float lastPositionOffsetX = tempMoveX * move.breakableCheckOffsetX;
        float lastPositionOffsetY = tempMoveY * move.breakableCheckOffsetY;

        
        //Debug.Log(move.lastMovementDirection.x + " " + move.lastMovementDirection.y);
        Vector2 rayStart = new Vector2((transform.position.x + lastPositionOffsetX), (transform.position.y + lastPositionOffsetY));  

        LayerMask breakableMask = LayerMask.GetMask("Breakable");
        RaycastHit2D hit = Physics2D.BoxCast(rayStart, move.boxSize, 0, move.lastMovementDirection, move.breakableCheckDistance, breakableMask);

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
