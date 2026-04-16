using UnityEngine;

public class MonsterMovement : BasicMovement
{

  public float speed = 3.0f;
  public float stopDistance = 0.5f;
  private Rigidbody2D rb2d;
  private Transform playerTransform;

  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();  
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    
    if(player != null)
    {
      playerTransform = player.transform;
    }
  }

  void FixedUpdate()
  {
    if(playerTransform == null) return;
    
    Vector2 direction = (Vector2)playerTransform.position - rb2d.position;
    float distance = direction.magnitude;
    
    Vector2 movement = Vector2.zero;
    
    if(distance > stopDistance)
    {
      movement = direction.normalized * speed * Time.fixedDeltaTime;
    }

    SetLastMove(movement);

    rb2d.MovePosition(rb2d.position + movement);
  }
}
