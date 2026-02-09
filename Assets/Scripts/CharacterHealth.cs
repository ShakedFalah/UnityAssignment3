using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        health = maxHealth;
    }
    public float TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        return HealthPercentage();
    }

    public float HealthPercentage()
    {
        return health / maxHealth;
    }

    public void HealToFull()
    {
        health = maxHealth;
    }
}
