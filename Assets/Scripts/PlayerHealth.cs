using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth = 0;

    private void Start()
    {
        // Falls du currentHealth im Inspector überschreiben willst, entferne diese Zeile:
        // currentHealth = 0; 
        Debug.Log("Startleben: " + currentHealth);
    }

    public void AddHealth(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;

            // Clamping, falls über max
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            Debug.Log("Leben um " + amount + " erhöht. Neuer Wert: " + currentHealth);
        }
        else
        {
            Debug.Log("Leben konnte nicht erhöht werden. Aktueller Wert: " + currentHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        Debug.Log("Schaden genommen. Neuer Wert: " + currentHealth);
    }
}
