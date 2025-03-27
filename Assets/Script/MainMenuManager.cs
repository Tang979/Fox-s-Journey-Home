using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene to load")]
    [SerializeField] private string gameplayScene = "GameManager";
    [SerializeField] private string levelScene = "lvl_0";
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void PlayGame()
    {
        Debug.Log("Play Game");
        // Load main gameplay scene
        scenesLoading.Add(SceneManager.LoadSceneAsync(gameplayScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(levelScene, LoadSceneMode.Additive));
    }
}
