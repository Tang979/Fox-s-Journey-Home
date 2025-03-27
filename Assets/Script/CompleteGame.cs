using UnityEngine;

public class CompleteGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gameManager = GameObject.FindFirstObjectByType<GameManager>();
        gameManager.Winner();
    }
}
