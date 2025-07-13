using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IDamageable
{
    public int health = 5;
    public float moveSpeed = 2f; 
    public float shootingInterval = 2f; 
    public GameObject projectilePrefab; 
    public Transform player; 
    public Sprite explosionSprite; // Assegna la sprite di esplosione da Inspector
    public float collisionDamage = 10f; // Danno da collisione modificabile

    private float nextShootTime;
    private bool hasExploded = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        nextShootTime = Time.time + shootingInterval; 
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!hasExploded)
        {
            MoveTowardsPlayer();
            HandleShooting();
        }
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
        if (hasExploded) return;
        health -= amount;
        if (health <= 0)
        {
            ExplodeAndDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasExploded) return;

        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && collision.CompareTag("Player"))
        {
            damageable.TakeDamage(Mathf.RoundToInt(collisionDamage));
            ExplodeAndDestroy();
        }
    }

    private void ExplodeAndDestroy()
    {
        hasExploded = true;
        if (spriteRenderer != null && explosionSprite != null)
        {
            spriteRenderer.sprite = explosionSprite;
        }
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }
        Destroy(gameObject, 0.2f);
    }
}