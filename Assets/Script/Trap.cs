using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerMovement player = GameObject.FindFirstObjectByType<PlayerMovement>();
        player.Die();
    }
}