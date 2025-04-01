using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float distance = 5f;
    public float height = 3f;
    public float rotationSpeed = 25f;

    private Vector3 currentOffset;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").transform; // Automatically finds the object named "Player"
        }
    }


    void LateUpdate()
    {
        // Rotate the camera around the player
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            currentOffset = Quaternion.AngleAxis(horizontal, Vector3.up) * currentOffset;
        }

        // Smoothly follow the player
        Vector3 targetPosition = player.position + currentOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2f);
        transform.LookAt(player);
    }

    public void ChangeView(Vector3 newOffset)
    {
        StartCoroutine(SmoothChangeView(newOffset));
    }

    private System.Collections.IEnumerator SmoothChangeView(Vector3 newOffset)
    {
        Vector3 startOffset = currentOffset;
        float timeElapsed = 0f;
        float duration = 1f; // Duration of the smooth transition

        while (timeElapsed < duration)
        {
            currentOffset = Vector3.Lerp(startOffset, newOffset, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        currentOffset = newOffset;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeView(new Vector3(0, height, distance)); // Switch to a new offset
        }
    }

}