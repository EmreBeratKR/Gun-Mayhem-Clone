using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideChecker : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask platform_Layer;
    private BoxCollider2D box;


    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (rb.velocity.y > 0)
        {
            cancel_Collision();
        }
        else if (!rb.GetComponent<PlayerMovement>().goingDown)
        {
            rb.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    
    private void cancel_Collision()
    {
        rb.GetComponent<BoxCollider2D>().enabled = !(bool)Physics2D.BoxCast(transform.position, box.size, 0f, Vector2.zero, 0f, platform_Layer);
    }
}
