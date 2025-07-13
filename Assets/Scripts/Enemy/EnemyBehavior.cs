using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IDamageable
{
    public int health = 5;
    public float moveSpeed = 2f; 
    public float shootingInterval = 2f; 
    public GameObject projectilePrefab; 
    public Transform player; 

    private float nextShootTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        nextShootTime = Time.time + shootingInterval; 
    }

    void Update()
    {
        MoveTowardsPlayer();
        HandleShooting();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void HandleShooting()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootingInterval; 
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.linearVelocity = direction * 5f;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}