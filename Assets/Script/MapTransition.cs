using Unity.Cinemachine;
using UnityEngine;

public class MapTrasition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform spawnPoint;
    private PolygonCollider2D bound;
    void Awake()
    {
        bound = GetComponent<PolygonCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CinemachineConfiner2D confiner2D = FindFirstObjectByType<CinemachineConfiner2D>();
        if (confiner2D != null)
        {
            confiner2D.BoundingShape2D = bound;
            confiner2D.InvalidateBoundingShapeCache();
            UpdateSpawnPoint();
            
        }
        }
    }
    private void UpdateSpawnPoint()
    {
        Vector2 startPostion = spawnPoint.position;
        Debug.Log("Start Point: " + startPostion);
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.setSpawnPoint(startPostion);
    }
}
