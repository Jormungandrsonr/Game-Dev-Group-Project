using UnityEngine;

public class PlayerMovement : BasicMovement
{
    public float speed = 5;

    Rigidbody2D rb2d;
    Animator animator;
    SpriteRenderer sr;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //We're going to use proper animations
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            
        }//end if
        else
        {
            
        }
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
        bool isMoving = force.magnitude > 0.001f;
        //animator.SetBool("isWalking", isMoving);
        
        rb2d.MovePosition(rb2d.position + force);
    }//end method
}
