using UnityEngine;

public class EnemyXPManager : MonoBehaviour
{
    public int experienceReward = 20;

    private void OnDestroy()
    {
        Player player = Object.FindFirstObjectByType<Player>();
        if (player != null)
        {
            player.AddExperience(experienceReward);
        }
    }
}