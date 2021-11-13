using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundChecker groundChecker;
    private bool isFalling;


    private void Update()
    {
        set_Speed();
        fall();
        idle();
    }

    private void set_Speed()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    public void jump()
    {
        animator.SetTrigger("Jump");
    }

    private void fall()
    {
        if (!isFalling)
        {
            if (rb.velocity.y < 0f)
            {
                animator.SetTrigger("Fall");
                isFalling = true;
            }
        }
        else if (rb.velocity.y == 0f)
        {
            isFalling = false;
        }
    }

    private void idle()
    {
        if (groundChecker.isGrounded())
        {
            animator.SetTrigger("Idle");
        }
    }
}
