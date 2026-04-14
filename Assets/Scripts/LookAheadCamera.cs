using UnityEngine;

public class LookAheadCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.5f;
    public Vector3 offset = new Vector3(0,0,-10);
    public float lookAheadDistance = 0.4f;
    public float lookAheadSpeed = 1f;

    float tempReturnSpeed;
    Vector3 currentLookAhead;
    Rigidbody2D targetRB;
    BasicMovement move;
   
    void Awake()
    {
        targetRB = target.GetComponent<Rigidbody2D>();
        move = target.GetComponent<BasicMovement>();
    }

    
    void FixedUpdate()
    {
        if(target == null)
        {
            return;
        }
        Vector3 targetLookAhead = new Vector3(move.currentMovementDirection.x, 
                                        move.currentMovementDirection.y, 0) * lookAheadDistance;

        currentLookAhead = Vector3.Lerp(
            currentLookAhead, targetLookAhead, lookAheadSpeed * Time.fixedDeltaTime
        );
        Vector3 desiredPosition = target.position + offset + currentLookAhead;
        if(targetLookAhead == Vector3.zero)
        {
            tempReturnSpeed = smoothSpeed;
            smoothSpeed *= 2;
        }
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime
        );
        if(targetLookAhead == Vector3.zero)
        {
            smoothSpeed = tempReturnSpeed;
        }
        transform.position = smoothedPosition;
    }
}
