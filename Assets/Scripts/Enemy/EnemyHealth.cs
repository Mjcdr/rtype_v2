using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Bullet"))
        {
            // Call the method to handle death
            HandleDeath();
            // Destroy the bullet
            Destroy(other.gameObject);
        }
    }

    private void HandleDeath()
    {
        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}