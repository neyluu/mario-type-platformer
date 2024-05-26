using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] public Vector2 position;
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] public float jumpPower = 16f;
    [SerializeField] public bool isMoving = false;
    [SerializeField] public bool isRunning = false;

    [Header("Combat")]
    [SerializeField] public int health = 100;
    [SerializeField] public float stamina = 100;
    [SerializeField] public int maxStamina = 100;
}
