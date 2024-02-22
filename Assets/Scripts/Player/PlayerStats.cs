using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rigidBody;
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] public float jumpPower = 16f;
}
