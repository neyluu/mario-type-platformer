using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float runningMultiplier = 2f;
    
    private bool isFacingRight = true;
    private float horizontalMove;
    private bool isRunning = false;

    void Start()
    {
        playerStats.rigidBody.freezeRotation = true;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        //RUNNING
        if(Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded())
        {
            if(!isRunning)
            {
                playerStats.moveSpeed *= runningMultiplier;
                isRunning = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && isRunning)
        {
            if(runningMultiplier != 0)
            {
                playerStats.moveSpeed /= runningMultiplier;
                isRunning = false;
            }
        }

        //JUMPING
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerStats.rigidBody.velocity = new Vector2(playerStats.rigidBody.velocity.x, playerStats.jumpPower);
        }

        if(Input.GetButtonUp("Jump") && playerStats.rigidBody.velocity.y > 0f)
        {
            playerStats.rigidBody.velocity = new Vector2(playerStats.rigidBody.velocity.x, playerStats.rigidBody.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        playerStats.rigidBody.velocity = new Vector2(horizontalMove * playerStats.moveSpeed, playerStats.rigidBody.velocity.y);
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
