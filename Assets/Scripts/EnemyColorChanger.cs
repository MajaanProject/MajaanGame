using UnityEngine;

public class EnemyColorChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnRed()
    {
        // Get the Renderer component from the new cube
       var cubeRenderer = GetComponent<Renderer>();

       // Use SetColor to set the main color shader property
       //cubeRenderer.material.SetColor("_Color", Color.red);
       // If your project uses URP, uncomment the following line and use it instead of the previous line
       cubeRenderer.material.SetColor("_BaseColor", Color.red);
    }

    public void TurnBlue()
    {
        var cubeRenderer = GetComponent<Renderer>();
        
        cubeRenderer.material.SetColor("_BaseColor", Color.blue);
    }

    public void TurnGreen()
    {
        var cubeRenderer = GetComponent<Renderer>();
        
        cubeRenderer.material.SetColor("_BaseColor", Color.green);
    }

    public void TurnYellow()
    {
        var cubeRenderer = GetComponent<Renderer>();
        
        cubeRenderer.material.SetColor("_BaseColor", Color.yellow);
    }
}
