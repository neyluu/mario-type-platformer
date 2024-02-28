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
    [SerializeField] private float cameraZoomSmoothing = .02f;

    void Start()
    {
        //Setting default camera size
        mainCamera.orthographicSize = defaultCameraSize;
    }

    void LateUpdate()
    {   
        //Following player
        cameraRigidBody.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);


        //Camera zooming when player is running
        if(player.isRunning && player.isMoving)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraZoomWhenRunning, cameraZoomSmoothing);
        }
        else
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, defaultCameraSize, cameraZoomSmoothing);
        }
    }
}
