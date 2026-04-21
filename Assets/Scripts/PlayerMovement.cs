using UnityEngine;

public class PlayerMovement : BasicMovement
{
    public float speed = 5;

    Rigidbody2D rb2d;
    Animator animator;
    SpriteRenderer sprite;
    bool lookRightBool = false;
    bool flipBool = false;
    bool forwardBool = true;


    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 force = Vector2.zero;

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            force.y += speed * Time.fixedDeltaTime;
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            force.y -= speed * Time.fixedDeltaTime;
            
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            force.x += speed * Time.fixedDeltaTime;
            
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            force.x -= speed * Time.fixedDeltaTime;
            
        }

        

        SetLastMove(force);
        if(currentMovementDirection != Vector2.zero && currentMovementDirection != lastMovementDirection)
        {
            lookRightBool = currentMovementDirection.x != 0;
            flipBool = currentMovementDirection.x < 0;
            forwardBool = currentMovementDirection.y <= 0; 
        }
        else
        {
            lookRightBool = lastMovementDirection.x != 0;
            flipBool = lastMovementDirection.x < 0;
            forwardBool = lastMovementDirection.y <= 0;   
        }
        

        

        //order for pipelining
        animator.SetBool("lookRight", lookRightBool);
        animator.SetBool("forward", forwardBool);

        bool isMoving = currentMovementDirection.magnitude > 0.001f;

        animator.SetBool("backwards", !forwardBool);
        sprite.flipX = flipBool;
        
        
        
        animator.SetBool("isWalking", isMoving);

        //Debug.Log("Is Moving " + isMoving +" Right "+ lookRightBool + " Left " + flipBool + " Forward " + forwardBool);
        
        rb2d.MovePosition(rb2d.position + force);
    }//end method
}
