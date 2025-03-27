using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private SceneField[] loadScene;
    [SerializeField] private SceneField[] unloadScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadScene();
            UnloadScene();
        }
    }

    private void LoadScene()
    {
        for (int i = 0; i < loadScene.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene scene = SceneManager.GetSceneAt(j);
                if (scene.name == loadScene[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(loadScene[i], LoadSceneMode.Additive);
            }
        }
    }

    private void UnloadScene()
    {
        for (int i = 0; i < unloadScene.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene scene = SceneManager.GetSceneAt(j);
                if (scene.name == unloadScene[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(unloadScene[i]);
                }
            }
        }
    }

}
