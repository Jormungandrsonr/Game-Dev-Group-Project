using UnityEngine;

public class PlayerAnim : BasicMovement
{

    Animator anim;
    SpriteRenderer sprite;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

    }
    // Update is called once per frame
    void Update()
    {
        if(currentMovementDirection.x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
