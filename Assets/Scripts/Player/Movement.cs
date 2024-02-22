using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] public Rigidbody2D playerRigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float runningMultiplier = 2f;
    
    private bool isFacingRight = true;
    private float horizontalMove;

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
            if(!playerStats.isRunning)
            {
                playerStats.moveSpeed *= runningMultiplier;
                playerStats.isRunning = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && playerStats.isRunning)
        {
            if(runningMultiplier != 0)
            {
                playerStats.moveSpeed /= runningMultiplier;
                playerStats.isRunning = false;
            }
        }

        //JUMPING
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
           playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerStats.jumpPower);
        }

        if(Input.GetButtonUp("Jump") && playerRigidBody.velocity.y > 0f)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y * 0.5f);
        }

        Flip();

        playerStats.positionX = playerRigidBody.position.x;
        playerStats.positionY = playerRigidBody.position.y;
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(horizontalMove * playerStats.moveSpeed, playerRigidBody.velocity.y);
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
