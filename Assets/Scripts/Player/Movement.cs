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
        if(Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded() && player.stamina > 20)
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
        if(player.isRunning && player.stamina < 1)
        {
            player.moveSpeed /= runningMultiplier;
            player.isRunning = false;
        }

        //JUMPING
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, player.jumpPower);
            UseStamina(5);
        }

        //Jumping depending on time when space is pressed, currently disabled
        // if(Input.GetButtonUp("Jump") && playerRigidBody.velocity.y > 0f)
        // {
        //     playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.y * 0.5f);
        // }

        Flip();

        //Saving current coordinats in playerStats
        player.position.x = playerRigidBody.position.x;
        player.position.y = playerRigidBody.position.y;
    }

    private void FixedUpdate()
    {
        //Player horizontal movement
        Vector2 playerVelocity = playerRigidBody.velocity;
        playerVelocity.x = horizontalMove * player.moveSpeed;
        playerRigidBody.velocity = playerVelocity;

        //Checking is player moving
        if(horizontalMove != 0) player.isMoving = true;
        else player.isMoving = false;

        StaminaMenagment();
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

    private void StaminaMenagment()
    {
        if(player.isRunning && player.isMoving)
        {
            UseStamina(0.1f);
        }
        else
        {
            GetStamina(0.05f);
        }
    }

    private void UseStamina(float amount)
    {
        if(player.stamina - amount > 0) player.stamina -= amount;
        else player.stamina = 0;
    }

    private void GetStamina(float amount)
    {
        if(player.stamina + amount < player.maxStamina) player.stamina += amount;
        else player.stamina = player.maxStamina;
    }
}
