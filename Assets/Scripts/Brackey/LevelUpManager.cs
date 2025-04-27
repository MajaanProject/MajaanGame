using UnityEngine;

public class LevelUpManager: MonoBehaviour
{
    public int currentLevel = 1;
    public int experiencePoints = 0;
    public int experienceToLevelUp = 100;

    public void AddExperience(int amount)
    {
        experiencePoints += amount;

        if (experiencePoints >= experienceToLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        experiencePoints -= experienceToLevelUp;
        Debug.Log("Level Up! Current Level: " + currentLevel);

        if (currentLevel == 3)
        {
            Debug.Log("Congratulations! You've reached Level 3.");
        }
    }
}