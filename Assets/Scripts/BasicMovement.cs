using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Vector2 lastMovementDirection = Vector2.down;
    public Vector2 currentMovementDirection = Vector2.zero;
    public Vector2 boxSize = new Vector2(0.08f,0.001f);
    public float breakableCheckDistance = 0.01f;
    public float breakableCheckOffsetX = 0.1f;
    public float breakableCheckOffsetY = 0.2f;

    public void SetLastMove(Vector2 movement)
    {
        if(currentMovementDirection != Vector2.zero)
        {
            lastMovementDirection = currentMovementDirection.normalized;
        }
        if(movement != Vector2.zero)
        {
            currentMovementDirection = movement.normalized;
        }
        else
        {
           
            currentMovementDirection = Vector2.zero;
        }
    }
    
    /*
    public bool IsGrounded()
    {
        Collider2D col = GetComponent<Collider2D>();
        Vector2 rayStart = new Vector2(transform.position.x, transform.position.y - groundCheckOffset);
        RaycastHit2D hit = Physics2D.BoxCast(rayStart, boxSize, 0, Vector2.down, groundCheckDistance);

        bool grounded = hit.collider != null;
        //Debug.DrawRay(rayStart, Vector2.down*groundCheckDistance, grounded ? Color.blue: Color.red);
        
        if(grounded)
        {
            Debug.DrawRay(rayStart, Vector2.down*groundCheckDistance,Color.red);
        }
        else
        {
            Debug.DrawRay(rayStart, Vector2.down*groundCheckDistance, Color.blue);
        }
        
        
        return grounded;
    }
    */
}
