using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance; // Singleton pattern

    private Vector3 respawnPosition;
    private Quaternion respawnRotation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPosition = newRespawnPoint.position;
        respawnRotation = newRespawnPoint.rotation;
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = respawnPosition;
        player.transform.rotation = respawnRotation;
        Debug.Log("Player respawned at: " + respawnPosition);
    }
}