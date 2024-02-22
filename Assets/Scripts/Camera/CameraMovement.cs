using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
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

    void Start()
    {
        mainCamera.orthographicSize = defaultCameraSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        cameraRigidBody.position = new Vector3(player.positionX + offset.x, player.positionY + offset.y, offset.z);

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
