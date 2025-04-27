using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; } //Singleton pattern

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Go to the next level based on build index
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) //Ensure valid next level
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels available! Reached the last level.");
        }
    }

    //Load a specific scene by its name
    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName) && SceneExists(sceneName)) //Check if scene exists
        {
            SceneManager.LoadSceneAsync(sceneName); //Load scene asynchronously
        }
        else
        {
            Debug.LogWarning($"Scene '{sceneName}' cannot be loaded. Verify scene name and build settings.");
        }
    }

    //Helper function to validate if the scene exists
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string extractedName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (extractedName == sceneName)
            {
                return true; //Scene found
            }
        }
        return false; //Scene not found
    }

    //Reload the current scene
    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    //Quit the game (optional for UI implementation)
    public void QuitGame()
    {
        Debug.Log("Quit Game triggered.");
        Application.Quit(); //Works only in a built application
    }
}