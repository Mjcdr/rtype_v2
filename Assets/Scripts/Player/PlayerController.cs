using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f; 
    public float attack_Timer = 0.3f;
    private float current_Attack_Timer;
    private bool canAttack;

    [SerializeField]
    private GameObject player_Bullet;

    [SerializeField]
    private Transform attack_Point;

    void Start()
    {
        current_Attack_Timer = attack_Timer;
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

}