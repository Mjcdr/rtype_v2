using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IDamageable
{
    public int health = 5;
    public float moveSpeed = 2f; 
    public float shootingInterval = 2f; 
    public GameObject projectilePrefab; 
    public Transform player; 
    public Sprite explosionSprite; 
    public float collisionDamage = 10f; 

    private float nextShootTime;
    private bool hasExploded = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;
    private bool isVisible = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        nextShootTime = Time.time + shootingInterval; 
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
    }

    private void OnBecameVisible()
    {
        Debug.Log("Nemico visibile!");
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Nemico NON visibile!");
        isVisible = false;
    }

    void Update()
    {
        if (!hasExploded)
        {
            if (!IsVisibleFromMainCamera())
            {
                MoveHorizontallyTowardsPlayer();
            }
            else
            {
                MoveTowardsPlayer();
                HandleShooting();
            }
        }
    }

    private bool IsVisibleFromMainCamera()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer == null) return false;
        var cam = Camera.main;
        if (cam == null) return false;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    private void MoveHorizontallyTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
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