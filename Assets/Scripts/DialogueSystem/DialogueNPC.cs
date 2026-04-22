using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [System.Serializable]
    public class DialogueCheckpoint
    {
        public DialogueLine[] dialogueLines;
    }

    public DialogueCheckpoint[] checkpoints; // 0 = first time, 1 = after first talk, etc.

    [HideInInspector]
    public int currentCheckpoint = 0; // tracks where we are

    private bool playerInRange = false;
    public bool dialogueStarted = false;

    private void Update()
    {
        if (playerInRange && !dialogueStarted && Input.GetKeyDown(KeyCode.E))
        {
            int index = Mathf.Clamp(currentCheckpoint, 0, checkpoints.Length - 1);
            DialogueManager.instance.StartDialogue(checkpoints[index].dialogueLines);
            dialogueStarted = true;
        }

        if (dialogueStarted && DialogueManager.instance.dialogueFinished)
            dialogueStarted = false;
    }

    // Call this from a UnityEvent on any DialogueChoice to advance to the next checkpoint
    public void AdvanceCheckpoint()
    {
        if (currentCheckpoint < checkpoints.Length - 1)
            currentCheckpoint++;
        Debug.Log(gameObject.name + " checkpoint advanced to " + currentCheckpoint);
    }

    // Call this if you want to jump to a specific checkpoint
    public void SetCheckpoint(int index)
    {
        currentCheckpoint = Mathf.Clamp(index, 0, checkpoints.Length - 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRange = false;
    }
}