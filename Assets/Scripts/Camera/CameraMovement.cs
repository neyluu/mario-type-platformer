using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Camera camera;
    [SerializeField] Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Camera " + playerStats.moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
