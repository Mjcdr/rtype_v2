using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour, IDamageable
{
    public int health = 100;
    public float floatAmplitude = 2f;
    public float floatSpeed = 1f;
    public float shootingInterval = 1f;
    public GameObject projectilePrefab;
    public Transform player;
    public Sprite explosionSprite;
    public string victorySceneName = "Victory";

    private float nextShootTime;
    private float startY;
    private bool hasExploded = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D bossCollider;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        startY = transform.position.y;
        nextShootTime = Time.time + shootingInterval;
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!hasExploded)
        {
            FloatVertically();
            HandleShooting();
        }
    }

    private void FloatVertically()
    {
        Vector3 pos = transform.position;
        pos.y = startY + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = pos;
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
        if (projectilePrefab != null && player != null)
        {
            // Direzione base verso il player
            Vector2 toPlayer = (player.position - transform.position).normalized;

            // Aggiungi casualità all'angolo (±30 gradi)
            float angleOffset = Random.Range(-30f, 30f);
            float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg + angleOffset;
            Vector2 shootDir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = shootDir * 7f;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (hasExploded) return;
        health -= amount;
        if (health <= 0)
        {
            ExplodeAndVictory();
        }
    }

    private void ExplodeAndVictory()
    {
        hasExploded = true;
        if (spriteRenderer != null && explosionSprite != null)
        {
            spriteRenderer.sprite = explosionSprite;
        }
        if (bossCollider != null)
        {
            bossCollider.enabled = false;
        }
        Invoke(nameof(GoToVictory), 0.5f);
        Destroy(gameObject, 0.5f);
    }

    private void GoToVictory()
    {
        SceneManager.LoadScene(victorySceneName);
    }
}