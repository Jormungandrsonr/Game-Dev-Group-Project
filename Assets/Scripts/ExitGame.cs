using UnityEditor;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject exitPanel; // assign in Inspector, set inactive by default
    private bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleExitMenu();
    }

    public void ToggleExitMenu()
    {
        isOpen = !isOpen;
        exitPanel.SetActive(isOpen);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        if(Application.isEditor)
        {EditorApplication.ExitPlaymode();}
        else{Application.Quit();}
    }
}