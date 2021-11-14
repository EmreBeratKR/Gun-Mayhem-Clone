using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private PlayerAnimation playerAnimation;
    private Rigidbody2D rb;
    private BoxCollider2D box_collider;
    [SerializeField, Range(5f, 30f)] private float maxSpeed;
    [SerializeField, Range(0f, 40f)] private float moveSpeed;
    [SerializeField, Range(0f, 40f)] private float jumpSpeed;
    [SerializeField, Range(0f, 20f)] private float fallSpeed;
    private int extraJump;
    public bool goingDown = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box_collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontalMove();
        jumpNfall();
    }

    private void horizontalMove()
    {
        // accelerates the player towards left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            // limits the horizontal speed
            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                rb.velocity += Vector2.left * (3f * moveSpeed) * Time.deltaTime;
                if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                {
                    rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
                }
            }
        }
        // accelerates the player towards right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            // limits the horizontal speed
            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                rb.velocity += Vector2.right * (3f * moveSpeed) * Time.deltaTime;
                if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                {
                    rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
                }
            }
        }
        // horizontal drag
        if (rb.velocity.x > 0f)
        {
            rb.velocity += Vector2.left * (2f * moveSpeed) * Time.deltaTime;
            if (rb.velocity.x < 0f)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
        else if (rb.velocity.x < 0f)
        {
            rb.velocity += Vector2.right * (2f * moveSpeed) * Time.deltaTime;
            if (rb.velocity.x > 0f)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
    }

    private void jumpNfall()
    {
        // resets jump count if player is grounded
        if (groundChecker.isGrounded() && rb.velocity.y == 0f)
        {
            extraJump = 1;
        }
        // jumps the player
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (groundChecker.isGrounded() && rb.velocity.y == 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                playerAnimation.jump();
            }
            else
            {
                if (extraJump > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    playerAnimation.jump();
                    extraJump--;
                }
            }
        }
        // increases the gravity if the player is falling
        if (rb.velocity.y > 0f)
        {
            rb.gravityScale = fallSpeed;
        }
        else if (rb.velocity.y < 0f)
        {
            rb.gravityScale = fallSpeed * 2;
        }

        // falls the player over from the standing platform
        if (Input.GetKeyDown(KeyCode.DownArrow) && groundChecker.isGrounded() && !goingDown)
        {
            StartCoroutine(go_Down());
        }
    }

    private IEnumerator go_Down()
    {
        // disables collision for given time
        Transform lastGround = groundChecker.currentGround();
        goingDown = true;
        box_collider.enabled = false;
        //yield return new WaitForSeconds(0.05f);
        while (true)
        {
            if (lastGround != groundChecker.currentGround() && groundChecker.isGrounded() && rb.velocity.y <= 0f)
            {
                box_collider.enabled = true;
                break;
            }
            else if (rb.velocity.y > 0)
            {
                break;
            }
            yield return null;
        }
        goingDown = false;
    }
}
