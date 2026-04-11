using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;

    // If this has lines, dialogue will branch into them after selection
    public DialogueTree branchTree;

    public UnityEvent onChoiceSelected;
}