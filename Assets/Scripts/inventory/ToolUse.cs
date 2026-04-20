using UnityEngine;
using System;


/*
    The purpose of this class is to be attached to the player character, and allow them to use tools to get resources.
    The class includes methods to RayCast for breakable objects, freezing the player to the object, 
    and giving them the resources from that object.
*/
public class ToolUse : MonoBehaviour
{
    Rigidbody2D rb2d;
    BasicMovement move;
    Collider2D collide;
    Transform breakableTransform;
    GameObject currentUse;
    TempLoot tempLootTable;
    System.Random rnd;
    
    bool breakRequest = false;
    bool usingTool = false;
    short objectHealth = 3;
    public float toolPosition = 0.5f;

    
    void Awake()
    {
        move = GetComponent<BasicMovement>();
        rb2d = GetComponent<Rigidbody2D>();
        rnd = new System.Random();
        
    }
    void Update()
    {
        //in update to get player input as quick as possible
        bool breakableInReach = IsFacingBreakable();
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(usingTool)
            {
                FinishTool();
            }
            else if(breakableInReach)
            {
                //Debug.Log("In reach.");
                breakRequest = true;
                if(breakableTransform == null)
                {
                    breakRequest = false;
                }
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
            ReadyTool();
            
            //Destroy(breakableTransform.gameObject);
            breakRequest = false;
        }
        //same thing with jump, but make it tool use with tag "breakable"
    }


    //temp GUI
    void OnGUI()
    {
        if(usingTool)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 48;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(10, 10, 200 ,50), "Press E " + tempLootTable.health + " Times!", style);
        }
    }

    ///////////Non-Unity-Based Methods/////////////

    /*
        Method takes the last direction faced by the player character
        and boxcasts a small distance in front of them.

        This method can be modularized if input was changed to LayerMask type.
    */
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

    /*
        This method gets the player ready to use a tool to get a resource. 
        This freezes the players position to the left of the obejct, 
        and will unfreeze the player if no loot table is attached to the object.
    */
    public void ReadyTool()  
    {
        usingTool = true;

        rb2d.position = breakableTransform.position - new Vector3(toolPosition,0,0);
        move.lastMovementDirection = new Vector2(1,0);
        rb2d.linearVelocity = Vector2.zero;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        currentUse = breakableTransform.gameObject;
        tempLootTable = currentUse.GetComponent<TempLoot>();
        
        if(tempLootTable == null)
        {
            Debug.Log("Loot Table not assigned to object");
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            usingTool = false;
            return;
        }
        objectHealth = tempLootTable.GetHealth(); 
        

        
        //freeze position
        //allow for animations
        //e is pressed three times
        //rock is destroyed
        //player is freed

    }

    /*
        This method will check if the object is ready to break, 
        and if it is, it will unfreeze the player and give them the allocated resources.
    */
    public void FinishTool()
    {
        
        
        if(objectHealth > 1)
        {
            objectHealth--;
        }
        else
        {
            usingTool = false;
            //IResource resource = currentUse.GetComponent<IResource>();
            short currentResourceType = (short)tempLootTable.GetResourceType();
            
            
            if(tempLootTable.fishable)
            {
                Debug.Log("Fish!");
                int tempFishCaught = rnd.Next(1,100);
                //10% for Fish 3, 30% for Fish 2, 60% for Fish 1
                if(tempFishCaught > 90)
                {tempFishCaught = 3;}
                else if(tempFishCaught > 60)
                {tempFishCaught = 2;}
                else
                {tempFishCaught = 1;}
                InventoryManager.AddItem(currentResourceType + tempFishCaught, tempLootTable.GetAmount());
            }
            else
            {
                InventoryManager.AddItem(currentResourceType, tempLootTable.GetAmount());
                //Debug.Log(InventoryManager.CheckItemCount(currentResourceType) + "stones");
            
                //maybe a destroy animation here?
                Destroy(currentUse);
            }
            //test lines
            
            //add a plus 1 with item animation to show what item was gained
            //Debug.Log(InventoryManager.wood + " " + InventoryManager.stone);
                
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            objectHealth = 3;
            
        }
    }


}
