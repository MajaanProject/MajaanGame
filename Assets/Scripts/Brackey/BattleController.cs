using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public Transform respawnPoint; // Respawn location in the main scene
    public GameObject player; // Reference to the player object

    public void EndBattle()
    {
        // Set the respawn point
        RespawnManager.Instance.SetRespawnPoint(respawnPoint);
        Debug.Log("Respawn point set to: " + respawnPoint.position);

        // Unload the additive battle scene
        SceneManager.UnloadSceneAsync("BattleScene");
        Debug.Log("Battle scene unloaded.");

        // Respawn the player
        RespawnManager.Instance.RespawnPlayer(player);

        Debug.Log("Battle ended and respawn point set.");
    }
}