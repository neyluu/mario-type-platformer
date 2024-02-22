using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats player;

    [SerializeField] private Rigidbody2D cameraRigidBody;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float cameraOffsetY = 1f;
    [SerializeField] private int defaultCameraSize = 5;
    [SerializeField] private float cameraZoomWhenRunning = 8;

    void Start()
    {
        mainCamera.orthographicSize = defaultCameraSize;
    }

    // Update is called once per frame
    void Update()
    {   
        cameraRigidBody.position = new Vector3(player.positionX, player.positionY + cameraOffsetY, 10);

        if(player.isRunning)
        {
            mainCamera.orthographicSize = cameraZoomWhenRunning;
        }
        else
        {
            mainCamera.orthographicSize = defaultCameraSize;
        }
    }
}
