using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats player;

    [SerializeField] private Rigidbody2D cameraRigidBody;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int defaultCameraSize = 5;
    [SerializeField] private float cameraZoomWhenRunning = 8;
    [SerializeField] private float cameraSmoothing = .02f;
    [SerializeField] private float cameraFollowSpeed = 9;

    void Start()
    {
        //Setting default camera size
        mainCamera.orthographicSize = defaultCameraSize;
    }

    void FixedUpdate()
    {
        //Following player        
        cameraRigidBody.position = new Vector3(
            Mathf.Lerp(cameraRigidBody.position.x, player.position.x + offset.x, cameraSmoothing),
            Mathf.Lerp(cameraRigidBody.position.y, player.position.y + offset.y, cameraSmoothing),
            offset.z
        );

        //Camera zooming when player is running
        if(player.isRunning && player.isMoving)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraZoomWhenRunning, cameraSmoothing);
        }
        else
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, defaultCameraSize, cameraSmoothing);
        }
    }
}
