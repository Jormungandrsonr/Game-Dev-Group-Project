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
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            sr.flipX = true;
        }//end if
        else
        {
            sr.flipX = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 force = Vector2.zero;

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            force.y += speed * Time.fixedDeltaTime;
            //transform.position += (Vector3)new Vector2(0,speed) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            force.y -= speed * Time.fixedDeltaTime;
            //transform.position += (Vector3)new Vector2(0,-speed) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            force.x += speed * Time.fixedDeltaTime;
            //transform.position += (Vector3)new Vector2(speed,0) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            force.x -= speed * Time.fixedDeltaTime;
            //transform.position += (Vector3)new Vector2(-speed,0) * Time.deltaTime;
        }

        SetLastMove(force);
        //Debug.Log("Force: " + force);
        bool isMoving = force.magnitude > 0.001f;
        animator.SetBool("isWalking", isMoving);
        
        rb2d.MovePosition(rb2d.position + force);
    }//end method
}
