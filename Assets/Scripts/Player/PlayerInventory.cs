using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    // Collectables
    [SerializeField] public int applesCount = 0;

    public void AppleCollected()
    {
        applesCount++;
    }
}
