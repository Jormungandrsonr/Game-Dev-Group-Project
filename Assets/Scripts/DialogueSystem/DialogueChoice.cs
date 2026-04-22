using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;

    public TradeOffer tradeOffer; //is there a resource check

    // If this has lines, dialogue will branch into them after selection
    public DialogueTree branchTree;

    public UnityEvent onChoiceSelected;
}