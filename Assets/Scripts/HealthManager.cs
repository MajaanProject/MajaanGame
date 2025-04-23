using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int _maxHealth;
    public int _minHealth;
    private int _health;

    public HealthBar healthBar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = _maxHealth;
		healthBar.SetMaxHealth(_maxHealth);
    }

    void Update()
    {
        var cubeRenderer = GetComponent<Renderer>();

        if (_health <= (_maxHealth * .1))
        {
            cubeRenderer.material.SetColor("_BaseColor", Color.red);
        } else if (_health <= (_maxHealth * .4)){
            cubeRenderer.material.SetColor("_BaseColor", Color.yellow);
        } else {
            cubeRenderer.material.SetColor("_BaseColor", Color.green);
        }
    }

    public void Damage(int _damage)
    {
        _health -= _damage;

        healthBar.SetHealth(_health);

        if (_health < _minHealth)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);
}

}
