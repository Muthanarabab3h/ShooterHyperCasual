// Updated TNTItem Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTItem : MonoBehaviour, InventoryItem
{
    public string Name => "TNT";
    public Sprite Image => tntSprite;
    public GameObject tntPrefab;
    public Sprite tntSprite;

    public void OnPickUp()
    {
        Debug.Log($"{Name} picked up!");
    }

    public void OnThrow(Vector3 spawnPosition)
    {
        Debug.Log($"TNT thrown at position: {spawnPosition}");

        spawnPosition.y = 0; // Ensure the Y position is fixed to 0
        if (tntPrefab != null)
        {
            Instantiate(tntPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("TNT prefab is not assigned!");
        }
    }
}
