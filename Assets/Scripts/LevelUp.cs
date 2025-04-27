using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    public Slider slider; // To display level progress
    public Gradient gradient; // For dynamic color changes
    public Image fill; // Fill image for the slider
    public Text levelText; // Optional: Text to show the current level

    private int currentLevel = 1; // Starting level

    public void SetMaxXP(int maxXP)
    {
        slider.maxValue = maxXP;
        slider.value = 0;

        fill.color = gradient.Evaluate(0f);
    }

    public void AddXP(int xp)
    {
        slider.value += xp;

        // Check if the XP slider is full (level up condition)
        if (slider.value >= slider.maxValue)
        {
            LevelUp();
        }

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void LevelUp()
    {
        currentLevel++;
        slider.value = 0; // Reset XP to 0
        slider.maxValue *= 1.5f; // Optionally increase the max XP requirement
        fill.color = gradient.Evaluate(0f);

        if (levelText != null)
        {
            levelText.text = "Level: " + currentLevel;
        }

        // Add any additional level-up behavior here (e.g., unlock abilities, play animation)
        Debug.Log("Level Up! Current Level: " + currentLevel);
    }
}
