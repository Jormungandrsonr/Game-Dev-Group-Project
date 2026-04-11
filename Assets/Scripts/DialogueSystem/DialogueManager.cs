using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    //SINGLETON: Can be called without declaration
    public static DialogueManager instance;

    [Header("Linked Components")]
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public Image speakerImage;
    public GameObject dialogueGameObject;

    // NEW: assign a panel in the Inspector; buttons are spawned here at runtime
    public GameObject choicePanel;
    public GameObject choiceButtonPrefab; // Button with a TextMeshProUGUI child

    [Header("Text Configuration")]
    public float typingSpeed = 0.05f;

    [Header("Dialogue Status")]
    public bool isTyping = false;
    public bool dialogueFinished = true;

    [Header("Dialogue")]
    public DialogueLine[] dialogueLines;

    #region PRIVATE VARIABLES
    private int currentIndex = 0;
    private Coroutine typingCoroutine;
    private bool justStarted = false;
    private bool waitingForChoice = false; // NEW
    #endregion

    private void Awake()
    {
        instance = this;
        dialogueGameObject.SetActive(false);
    }

    public void StartDialogue(DialogueLine[] newLines)
    {
        dialogueGameObject.SetActive(true);
        dialogueFinished = false;
        dialogueLines = newLines;
        currentIndex = 0;
        justStarted = true;
        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (justStarted)
            {
                justStarted = false;
                return;
            }

            // NEW: block E from advancing dialogue while a choice is displayed
            if (waitingForChoice) return;

            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                ShowFullLine(dialogueLines[currentIndex]);
                isTyping = false;
            }
            else
            {
                currentIndex++;

                if (currentIndex < dialogueLines.Length)
                {
                    typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
                }
                else
                {
                    dialogueFinished = true;
                    dialogueGameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator TypeLine(DialogueLine line)
    {
        isTyping = true;

        textBox.text = "";
        nameBox.text = line.speakerName;
        speakerImage.sprite = line.speakerImage;

        foreach (char c in line.dialogueText)
        {
            textBox.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        // NEW: show choices if this line has any
        if (line.choices != null && line.choices.Length > 0)
            ShowChoices(line.choices);
    }

    private void ShowFullLine(DialogueLine line)
    {
        textBox.text = line.dialogueText;
        nameBox.text = line.speakerName;
        speakerImage.sprite = line.speakerImage;
    }

    private void ShowChoices(DialogueChoice[] choices)
    {
        Debug.Log("ShowChoices called with " + choices.Length + " choices");
        waitingForChoice = true;
        choicePanel.SetActive(true);

        foreach (DialogueChoice choice in choices)
        {
            GameObject btn = Instantiate(choiceButtonPrefab, choicePanel.transform);
            Debug.Log("Spawned button for: " + choice.choiceText);

            Button b = btn.GetComponentInChildren<Button>();
            Debug.Log("Button component found: " + (b != null));

            TextMeshProUGUI tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log("TMP component found: " + (tmp != null));
            if (tmp != null) tmp.text = choice.choiceText;

            DialogueChoice captured = choice;
            if (b != null)
            {
                b.onClick.AddListener(() =>
                {
                    Debug.Log("Button clicked: " + captured.choiceText);
                    OnChoiceSelected(captured);
                });
            }
        }
    }

    private void OnChoiceSelected(DialogueChoice choice)
    {
        Debug.Log("OnChoiceSelected called for: " + choice.choiceText);
        Debug.Log("Branch tree is null: " + (choice.branchTree == null));

        foreach (Transform child in choicePanel.transform)
            Destroy(child.gameObject);
        choicePanel.SetActive(false);
        waitingForChoice = false;

        choice.onChoiceSelected?.Invoke();

        if (choice.branchTree != null && choice.branchTree.lines.Length > 0)
        {
            Debug.Log("Branching into tree with " + choice.branchTree.lines.Length + " lines");
            dialogueLines = choice.branchTree.lines;
            currentIndex = 0;
            typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
        }
        else
        {
            Debug.Log("No branch tree, ending dialogue");
            dialogueFinished = true;
            dialogueGameObject.SetActive(false);
        }
    }
}