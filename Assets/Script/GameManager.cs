using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab; // Prefab nhân vật
    private InputAction pauseAction;
    private bool isPaused;
    private GameObject playerInstance;
    [SerializeField] private Vector2 spawnPoint; // Vị trí spawn nhân vật

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;

    public void setSpawnPoint(Vector2 spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }
    void Start()
    {
        // Tìm nhân vật nếu đã tồn tại
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        pauseAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/Escape");
        pauseAction.Enable();

        // Nếu không tìm thấy, tạo mới từ Prefab
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        }
        gameOver.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void RespawnPlayer(){
        gameOver.SetActive(false);
        playerInstance = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        Time.timeScale = 1;
    }

    public void Winner()
    {
        winMenu.SetActive(true);
        isPaused = !isPaused;
    }

    public void PlayAgain()
    {
        MainMenuManager mainMenu = new MainMenuManager();
        Time.timeScale = 1;
        mainMenu.PlayGame();
    }

    void Update()
    {
        if(pauseAction.triggered)
            TogglePause();
        if(playerInstance == null)
        {
            // Tạo mới nhân vật nếu nhân vật bị hủy
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }
}
