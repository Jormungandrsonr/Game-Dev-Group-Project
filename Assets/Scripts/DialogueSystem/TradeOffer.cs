using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Trade Offer", menuName = "Dialogue/Trade Offer")]
public class TradeOffer : ScriptableObject
{
    [Header("Cost")]
    public EnumItemSet costItem;
    public int costAmount;

    [Header("Reward")]
    public EnumItemSet rewardItem;
    public int rewardAmount;

    [Header("Branches")]
    public DialogueTree successBranch;
    public DialogueTree failBranch;

    [Header("On Success Event (optional)")]
    public UnityEvent onSuccess;
}