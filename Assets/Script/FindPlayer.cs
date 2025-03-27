using Unity.Cinemachine;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject playerInstance;
    private CinemachineCamera cinemachineCamera;
    private void Update()
    {
        if (playerInstance != null)
        {
            return;
        }
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(playerInstance);
        cinemachineCamera = GetComponent<CinemachineCamera>();
        if (playerInstance != null)
        {
            cinemachineCamera.Follow = playerInstance.transform;
        }
    }
}
