using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpMult;
    [SerializeField]private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator animator;
    private float direction = 0;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        if (isGrounded())
        {
            body.velocity = new Vector2(direction * speed, body.velocity.y);
        } else
        {
            body.velocity = new Vector2(direction * (float) (speed / 1.5), body.velocity.y);
        }

        if (direction > 0.01f)
        {
            transform.localScale = Vector3.one;
        } 
        else if (direction < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, speed * jumpMult);
        }

        animator.SetBool("isRunning", direction != 0);

    }

    public bool canAttack()
    {
        return direction == 0;
    }
    
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
