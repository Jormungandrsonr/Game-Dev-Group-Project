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

    }
    void Update()
    {
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
        if(breakRequest)
        {
            Debug.Log("Breakable");
            //get the player to the position, have them do a lil dance, yk
            Destroy(breakableTransform.gameObject);
            breakRequest = false;
        }
        //same thing with jump, but make it tool use with tag "breakable"
    }

    ///////////Non-Unity-Based Methods/////////////

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
    }
}
