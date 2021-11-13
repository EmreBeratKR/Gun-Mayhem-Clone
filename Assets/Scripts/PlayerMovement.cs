using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private PlayerAnimation playerAnimation;
    private Rigidbody2D rb;
    private BoxCollider2D box_collider;
    [SerializeField, Range(5f, 20f)] private float maxSpeed;
    private int extraJump;
    public bool goingDown = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box_collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // resets jump count if player is grounded
        if (groundChecker.isGrounded())
        {
            extraJump = 1;
        }
        // adds force to the player which towards to left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                rb.AddForce(Vector2.left * 500f);
            }
        }
        // adds force to the player which towards to right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                rb.AddForce(Vector2.right * 500f);
            }
        }
        // limits the player's horizontal speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }
        }
        // adds force towards to up which makes the player jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (groundChecker.isGrounded())
            {
                rb.AddForce(Vector2.up * 4000f);
                playerAnimation.jump();
            }
            else
            {
                if (extraJump > 0)
                {
                    rb.AddForce(Vector2.up * 4000f);
                    playerAnimation.jump();
                    extraJump--;
                }
            }
        }
        // limits the player's jump speed
        if (rb.velocity.y > 50f) // check this value pls!!!
        {
            rb.velocity = new Vector2(rb.velocity.x, 30f);
        }
        // falls the player over from the standing platform
        if (Input.GetKeyDown(KeyCode.DownArrow) && groundChecker.isGrounded() && !goingDown)
        {
            StartCoroutine(go_Down());
        }
    }

    private IEnumerator go_Down()
    {
        goingDown = true;
        box_collider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        box_collider.enabled = true;
        goingDown = false;
    }
}
