using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float runningMultiplier = 2f;
    [SerializeField] private float jumpPower = 16f;
    [SerializeField] private bool isFacingRight = true;
    
    private float horizontalMove;
    public bool isRunning = false;

    void Start()
    {
        playerRigidBody.freezeRotation = true;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        //RUNNING
        if(Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded())
        {
            if(!isRunning)
            {
                moveSpeed *= runningMultiplier;
                isRunning = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && isRunning)
        {
            if(runningMultiplier != 0)
            {
                moveSpeed /= runningMultiplier;
                isRunning = false;
            }
        }

        //JUMPING
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpPower);
        }

        if(Input.GetButtonUp("Jump") && playerRigidBody.velocity.y > 0f)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(horizontalMove * moveSpeed, playerRigidBody.velocity.y);
    }

    //Fliping character when changing moving direction
    private void Flip()
    {
        if(isFacingRight && horizontalMove < 0f || !isFacingRight && horizontalMove > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}

