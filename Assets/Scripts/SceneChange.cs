using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string nextScene;
    //Scene currentScene;
    //Scene nextUpScene;
    public string spawnPointID; // ID of the spawn point in the next scene

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //currentScene = GetComponent<Scene>();
            SpawnManager.SetSpawn(spawnPointID);
            SceneManager.LoadSceneAsync(nextScene);
            //SceneManager.SetActiveScene(nextUpScene); 
            //SceneManager.UnloadSceneAsync(currentScene);
        }
    }

    public void LoadScene()
    {
        SpawnManager.SetSpawn(spawnPointID);
        SceneManager.LoadSceneAsync(nextScene);
    }

}
