using UnityEngine;

public class toolUse : MonoBehaviour
{
    Rigidbody2D rb2d;


    // Update is called once per frame
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        //raycast with box, check if tag, else have a question mark appear
        //simple tags on breakable objects/ocean spots
    }
    void FixedUpdate()
    {
        //same thing with jump, but make it tool use with tag "breakable"
    }
}
