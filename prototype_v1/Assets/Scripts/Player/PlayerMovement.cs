using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    public float speed;
    public bool moving = true;

    Vector3 movement;

    void Update()
    {
        if (moving)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

            spriteRenderer.flipX = movement.x >= 0 ? true : false;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);
        }
        else
        {
            movement = Vector3.zero;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);
        }

    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rigidBody.MovePosition(transform.position + movement.normalized * speed * Time.fixedDeltaTime);
        }
    }

    public void TogglePlayerMovement(bool value)
    {
        moving = value;
    }
}
