using UnityEngine;

public class Death : MonoBehaviour
{
    public Transform RespawnPoint;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            collider.transform.position = RespawnPoint.transform.position;
        }
    }
}
