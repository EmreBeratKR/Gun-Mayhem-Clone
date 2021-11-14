using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private float knockback;


    public void set_bullet(float k)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2((transform.rotation.y == 0 ? -1 : 1) * speed, 0f);
        knockback = k;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().velocity += (rb.velocity.x > 0 ? 1 : -1) * Vector2.right * knockback;
            Destroy(gameObject);
        }
    }
}
