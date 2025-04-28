using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int xp;
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public PlayerData playerData;
}