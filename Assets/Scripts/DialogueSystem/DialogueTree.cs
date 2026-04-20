using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Tree", menuName = "Dialogue/Dialogue Tree")]
public class DialogueTree : ScriptableObject
{
    public DialogueLine[] lines;
}