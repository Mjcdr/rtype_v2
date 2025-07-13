using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour, IDamageable
{

    public float moveSpeed = 5f; 
    public float attack_Timer = 0.3f;
    private float current_Attack_Timer;
    private bool canAttack;

    [SerializeField]
    private GameObject player_Bullet;

    [SerializeField]
    private Transform attack_Point;

    public int health = 20;
    [SerializeField] private Slider healthBar; 


    void Start()
    {
        current_Attack_Timer = attack_Timer;
        if (healthBar != null)
        {
            healthBar.maxValue = health;
            healthBar.value = health;
        }
    }


    void Update()

    {
        MovePlayer();
        Attack(); 
    }


    private void MovePlayer()

    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        ClampPosition();

    }


    private void ClampPosition()

    {
         float minX = -9f;
         float maxX = 9f;
         float minY = -5f;
         float maxY = 5f;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        transform.position = clampedPosition;

    }

    void Attack()
    {
        attack_Timer += Time.deltaTime; 
        if(attack_Timer > current_Attack_Timer)
        {
            canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                Instantiate(player_Bullet, attack_Point.position, Quaternion.identity); 
            }
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (healthBar != null)
            healthBar.value = health;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}