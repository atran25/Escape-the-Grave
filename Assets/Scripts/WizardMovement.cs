using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private float direction = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(direction * speed, body.velocity.y);

        if (direction > 0.01f)
        {
            transform.localScale = Vector3.one;
        } 
        else if (direction < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }

        animator.SetBool("isRunning", direction != 0);

    }

    public bool canAttack()
    {
        return direction == 0;
    }
}
