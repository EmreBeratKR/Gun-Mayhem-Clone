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
        move();
        fall();
        idle();
    }

    private void move()
    {
        bool isFiring = transform.parent.Find("Gun Slot").GetChild(0).GetComponent<Gun>().get_firing();
        bool isMoving = transform.parent.GetComponent<PlayerMovement>().isMoving;
        animator.SetBool("Move", (!isFiring && (Mathf.Abs(rb.velocity.x) > 0)) || (isFiring && isMoving));
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
