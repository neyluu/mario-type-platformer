using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerStats player;

    [SerializeField] public Rigidbody2D playerRigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float runningMultiplier = 2f;
    [SerializeField] private float runningSmoothing = 1f;
    
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
            if(!player.isRunning)
            {
                player.moveSpeed *= runningMultiplier;
                player.isRunning = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && player.isRunning)
        {
            if(runningMultiplier != 0)
            {
                player.moveSpeed /= runningMultiplier;
                player.isRunning = false;
            }
        }

        //JUMPING
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
           playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, player.jumpPower);
        }

        if(Input.GetButtonUp("Jump") && playerRigidBody.velocity.y > 0f)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y * 0.5f);
        }

        Flip();

        //Saving current coordinats in playerStats
        player.position.x = playerRigidBody.position.x;
        player.position.y = playerRigidBody.position.y;
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(horizontalMove * player.moveSpeed, playerRigidBody.velocity.y);
        
        //Checking is player moving
        if(horizontalMove != 0) player.isMoving = true;
        else player.isMoving = false;
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
