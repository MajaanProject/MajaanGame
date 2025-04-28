using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public PlayerStats playerStats; // Reference to your ScriptableObject

    void Awake()
    {
        // Load player data as soon as the scene starts
        LoadPlayerData();
    }

    void Start()
    {
        // Confirm data is synced with the ScriptableObject after loading
        SyncDataToScriptableObject();
    }

    public void SavePlayerData()
    {
        // Convert PlayerData (inside ScriptableObject) to JSON string
        string json = JsonUtility.ToJson(playerStats.playerData);
        // Save JSON string to a file
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
        Debug.Log("Data saved: " + json);
        Debug.Log("Data saving...");
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.json";

        if (File.Exists(path)) // Check if the save file exists
        {
            string json = File.ReadAllText(path); // Read JSON string from the file
            playerStats.playerData = JsonUtility.FromJson<PlayerData>(json); // Deserialize JSON into PlayerData structure
            Debug.Log($"Loaded Data - Health: {playerStats.playerData.health}, XP: {playerStats.playerData.xp}");
            Debug.Log("Loading data...");
            Debug.Log("Save path: " + Application.persistentDataPath);
        }
        else
        {
            Debug.LogWarning("Save file not found! Using default values.");
            playerStats.playerData.health = 100; // Default health value
            playerStats.playerData.xp = 0;       // Default XP value
        }
    }

    public void SyncDataToScriptableObject()
    {
        // Log confirmation for syncing (expand logic here if needed)
        Debug.Log($"Data synced to ScriptableObject - Health: {playerStats.playerData.health}, XP: {playerStats.playerData.xp}");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Press 'S' to save
        {
            SavePlayerData();
        }
        if (Input.GetKeyDown(KeyCode.L)) // Press 'L' to load
        {
            LoadPlayerData();
            SyncDataToScriptableObject(); // Sync again after loading
        }
    }
}