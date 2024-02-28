using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] public Vector2 position;
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] public float jumpPower = 16f;
    [SerializeField] public bool isMoving = false;
    [SerializeField] public bool isRunning = false;
}
