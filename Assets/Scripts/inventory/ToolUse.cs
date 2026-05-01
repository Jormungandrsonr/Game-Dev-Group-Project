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
    Animator anim;
    Transform breakableTransform;
    GameObject currentUse;
    TempLoot tempLootTable;
    System.Random rnd;
    
    bool breakRequest = false;
    bool usingTool = false;
    bool fishing = false;
    bool fishOnLine = false;
    short objectHealth = 3;
    short fishReel = 0;
    char currentReel = 'P';
    float currentTime;
    int currentTimeBlock;
    public float toolPosition = 0.5f;
    public float rodOffset = 0.5f;
    public float fishTime = 3f;

    
    void Awake()
    {
        move = GetComponent<BasicMovement>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rnd = new System.Random();
        
    }
    void Update()
    {
        //in update to get player input as quick as possible
        bool breakableInReach = IsFacingBreakable();
        if(fishing)
        {
            if(fishOnLine)
            {
                if(fishReel == 0 && Input.GetKeyDown(KeyCode.P))
                {
                    anim.SetTrigger("useRod");
                    currentReel = 'Y';
                    fishReel++;
                }
                else if (fishReel == 1 && Input.GetKeyDown(KeyCode.Y))
                {
                    anim.SetTrigger("useRod");
                    currentReel = 'R';
                    fishReel++;
                }
                else if (fishReel == 2 && Input.GetKeyDown(KeyCode.R))
                {
                    anim.SetTrigger("useRod");
                    currentReel = 'P';
                    CatchFish(2);
                    fishReel = 0;
                }
            }
            else if(FishOnLine())
            {
                fishOnLine = true;
            }
        }
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
            //Debug.Log("Breakable");
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
        GUIStyle style = new GUIStyle();
            style.fontSize = 48;
            style.normal.textColor = Color.red;
        if(usingTool)
        {
            GUI.Label(new Rect(10, 10, 200 ,50), "Press E " + tempLootTable.health + " Times!", style);
        }
        if(fishOnLine)
        {
            GUI.Label(new Rect(10, 10, 200 ,50), "Press " + currentReel + "!!!", style);
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
        PlayerAnim.Lock();

        anim.SetBool("finishTool",false);
        anim.SetBool("finishRod",false);

        currentUse = breakableTransform.gameObject;
        tempLootTable = currentUse.GetComponent<TempLoot>();
        
        if(tempLootTable == null)
        {
            Debug.Log("Loot Table not assigned to object");
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            usingTool = false;
            return;
        }
        if(tempLootTable.fishable)
        {
            rb2d.position = breakableTransform.position - new Vector3(toolPosition + rodOffset,0,0);
            usingTool = false;
            fishing = true;
            CastRod();
            anim.SetBool("usingRod", true);
            anim.SetTrigger("castRod");
        }
        else{anim.SetBool("usingTool", true);}
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
        anim.SetTrigger("useTool");

        if(objectHealth > 1)
        {
            objectHealth--;
        }
        else
        {
            usingTool = false;
            anim.SetBool("usingTool", usingTool);
            
            //IResource resource = currentUse.GetComponent<IResource>();
            short currentResourceType = (short)tempLootTable.GetResourceType();
            
            
            if(tempLootTable.fishable)
            {
                CatchFish(currentResourceType);
            }
            else
            {
                InventoryManager.AddItem(currentResourceType, tempLootTable.GetAmount());
                //Debug.Log(InventoryManager.CheckItemCount(currentResourceType) + "stones");
                anim.SetBool("finishTool",true);
                //maybe a destroy animation here?

                //I changed this to have resources not reload on scene change. Peter
                MinableResource minable = currentUse.GetComponent<MinableResource>();
                if (minable != null)
                    minable.OnMined();
                else
                    Destroy(currentUse);
            }
            //test lines
            
            //add a plus 1 with item animation to show what item was gained
            //Debug.Log(InventoryManager.wood + " " + InventoryManager.stone);
                
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            PlayerAnim.Unlock();
            objectHealth = 3;
            
        }
    }

    //have it cast the rod, 
    //make the time and time interval into solid things 
    //make it wait a certain time
    //animation, quick time event (p[ull], y[ank] ,r[eel])
    public void CastRod()
    {
        currentTime = DayTime.GetCurrentTime();
        currentTimeBlock = DayTime.GetCurrentTimeBlock();
        currentTime += (currentTimeBlock * GameTime.timeInterval);
    }

    public bool FishOnLine()
    {
        float tempTime = DayTime.GetCurrentTime();
        tempTime += DayTime.GetCurrentTimeBlock() * GameTime.timeInterval;
        tempTime -= currentTime;
        if(tempTime >= fishTime)
        {
            return true;
        }
        return false;
    }

    public void CatchFish(short currentResourceType)
    {
            //Debug.Log("Fish!");
            int tempFishCaught = rnd.Next(1,100);
            //10% for Fish 3, 30% for Fish 2, 60% for Fish 1
            if(tempFishCaught > 90)
            {tempFishCaught = 3;}
            else if(tempFishCaught > 60)
            {tempFishCaught = 2;}
            else
            {tempFishCaught = 1;}
            InventoryManager.AddItem(currentResourceType + tempFishCaught, tempLootTable.GetAmount());
            anim.SetBool("finishRod", true);
            fishing = false;
            fishOnLine = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            PlayerAnim.Unlock();
    }


}
