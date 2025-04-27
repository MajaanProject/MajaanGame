using System.Collections;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
    [SerializeField] float delayDuration = 2.0f; // Delay duration in seconds

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (goNextLevel)
        {
            StartCoroutine(DelayedNextLevel());
        }
        else
        {
            StartCoroutine(DelayedLoadScene());
        }
    }

    private IEnumerator DelayedNextLevel()
    {
        yield return new WaitForSeconds(delayDuration); // Wait for the delay duration
        SceneController.Instance.NextLevel(); // Proceed to the next level
    }

    private IEnumerator DelayedLoadScene()
    {
        yield return new WaitForSeconds(delayDuration); // Wait for the delay duration
        SceneController.Instance.LoadScene(levelName); // Load the specified scene
    }
}