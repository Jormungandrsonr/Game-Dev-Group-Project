using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string nextScene;
    //Scene currentScene;
    //Scene nextUpScene;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //currentScene = GetComponent<Scene>();
            SceneManager.LoadSceneAsync(nextScene);
            //SceneManager.SetActiveScene(nextUpScene); 
            //SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}
