using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpPower = 16f;
    [SerializeField] private bool isFacingRight = true;
    
    private float horizontalMove;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        Flip();

        if(Input.GetKeyDown("space"))
        {
            playerRigidBody.velocity = new Vector2(horizontalMove * moveSpeed, jumpPower);
        }
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(horizontalMove * moveSpeed, playerRigidBody.velocity.y);
    }

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

}
