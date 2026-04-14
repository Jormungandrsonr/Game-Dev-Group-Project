using UnityEngine;

public class ToolUse : MonoBehaviour
{
    Rigidbody2D rb2d;
    BasicMovement move;
    Collider2D collide;
    Transform breakableTransform;
    bool breakRequest = false;


    
    void Awake()
    {
        move = GetComponent<BasicMovement>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //in update to get player input as quick as possible
        bool breakableInReach = IsFacingBreakable();
        if(breakableInReach && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("In reach.");
            breakRequest = true;
            if(breakableTransform == null)
            {
                breakRequest = false;
            }
        } 
        else
        {
            //Debug.Log("Out of Reach");
        }
        
        //simple tags on breakable objects/ocean spots
    }
    void FixedUpdate()
    {
        //fixedupdate to use game logic not based on fps
        if(breakRequest)
        {
            Debug.Log("Breakable");
            //get the player to the position, have them do a lil dance, yk
            RockSmash(rb2d);
            
            //Destroy(breakableTransform.gameObject);
            breakRequest = false;
        }
        //same thing with jump, but make it tool use with tag "breakable"
    }

    ///////////Non-Unity-Based Methods/////////////

    public bool IsFacingBreakable()
    {
        //making sure that i call functions as least as possible. not super necc. but we ball
        float tempMoveX = move.lastMovementDirection.x;
        float tempMoveY = move.lastMovementDirection.y;

        //takes the offset and multiplies it by the tempMove to determine whether the boxcast should be 
        //in the postive or negative direction. 
        float lastPositionOffsetX = tempMoveX * move.breakableCheckOffsetX;
        float lastPositionOffsetY = tempMoveY * move.breakableCheckOffsetY;

        
        //Debug.Log(move.lastMovementDirection.x + " " + move.lastMovementDirection.y);
        //
        Vector2 rayStart = new Vector2((transform.position.x + lastPositionOffsetX), (transform.position.y + lastPositionOffsetY));  

        //make sure all breakable objects/tool use objects have this tag. 
        //the raycast will only accept this tag
        LayerMask breakableMask = LayerMask.GetMask("Breakable");
        RaycastHit2D hit = Physics2D.BoxCast(rayStart, move.boxSize, 0, move.lastMovementDirection, move.breakableCheckDistance, breakableMask);

        bool grounded = hit.collider != null;
        
        
        
        Debug.DrawRay(rayStart, move.lastMovementDirection*move.breakableCheckDistance, grounded ? Color.blue: Color.red);
        

        //Gets the Transform object of whatever the boxcast hits
        if(grounded)
        {
            //Debug.Log(hit.collider.tag);
            //grounded = hit.collider.tag != "Player";
            //if(grounded) {Debug.DrawRay(rayStart, move.lastMovementDirection*move.breakableCheckDistance,Color.red);}
            breakableTransform = hit.transform;
        }
        else
        {
            Debug.DrawRay(rayStart, move.lastMovementDirection*move.breakableCheckDistance, Color.green);
            breakableTransform = null;
        }
        
        return grounded;
    }//end method

    public void RockSmash(Rigidbody2D playerRB2D)  
    {

    }


}
