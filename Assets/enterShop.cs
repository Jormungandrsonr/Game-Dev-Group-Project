using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public GameObject promptObject;   // The sprite that says "Press E"
    private Transform playerTransform;
    private bool playerInRange = false;

    void Start()
    {
        promptObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && playerTransform != null)
        {
            // Follow above the player's head
            Vector3 offset = new Vector3(0, 1.5f, 0);
            promptObject.transform.position = playerTransform.position + offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerTransform = other.transform;
            promptObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerTransform = null;
            promptObject.SetActive(false);
        }
    }
}
