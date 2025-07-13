using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction=new Vector2(1,0);
    public float speed=2;

    public Vector2 velocity;
    void Start()
    {
        Destroy(gameObject,4);
    }

    void Update()
    {
        velocity=direction*speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos=transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }
}
