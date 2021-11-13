using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask platform_Layer;
    private BoxCollider2D box;


    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }
    
    public bool isGrounded()
    {
        return (bool)Physics2D.BoxCast(transform.position, box.size, 0f, Vector2.down, 0.1f, platform_Layer);
    }
}
