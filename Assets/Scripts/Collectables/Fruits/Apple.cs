using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private bool active = true;

    private void Awake()
    {
        gameObject.SetActive(active);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(inventory != null && active)
        {
            inventory.AppleCollected();
            gameObject.SetActive(false);
            active = false;
        }
    }
    
}
