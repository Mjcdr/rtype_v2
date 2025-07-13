using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 5;
    public int damage = 1;
    public Sprite explosionSprite; 

    public Vector2 velocity;
    private bool hasExploded = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D bulletCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletCollider = GetComponent<Collider2D>();
        Destroy(gameObject, 4);
    }

    void Update()
    {
        if (!hasExploded)
            velocity = direction * speed;
        else
            velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!hasExploded)
        {
            Vector2 pos = transform.position;
            pos += velocity * Time.fixedDeltaTime;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasExploded) return;

        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Explode();
            return;
        }
    }

    private void Explode()
    {
        hasExploded = true;
        if (spriteRenderer != null && explosionSprite != null)
        {
            spriteRenderer.sprite = explosionSprite;
        }
        if (bulletCollider != null)
        {
            bulletCollider.enabled = false;
        }
        Destroy(gameObject, 0.2f); // tempo per sprite di esplosione
    }
}
