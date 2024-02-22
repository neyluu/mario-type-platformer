using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Rigidbody2D cameraRigidBody;

    private float horizontalMove;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        horizontalMove = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        cameraRigidBody.velocity = new Vector2(horizontalMove * playerStats.moveSpeed, cameraRigidBody.velocity.y);
    }
}
