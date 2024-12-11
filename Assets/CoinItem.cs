// Updated CoinItem Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour, InventoryItem
{
    public string Name => "Coin";
    public Sprite Image => coinSprite;
    public GameObject coinPrefab;
    public Sprite coinSprite;

    private Gamemanager gameManager;

    void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<Gamemanager>();
        if (gameManager == null)
        {
            Debug.LogError("Gamemanager not found in the scene!");
        }
    }

    public void OnPickUp()
    {
        Debug.Log($"{Name} picked up!");
    }

    public void OnThrow(Vector3 spawnPosition)
    {
        if (gameManager != null && gameManager.coins > 0)
        {
            // Deduct one coin from the player's total
            gameManager.AddCoin(-1);

            Debug.Log($"Coin thrown at position: {spawnPosition}");

            spawnPosition.y = 0.5f; // Ensure the Y position is fixed to 0
            if (coinPrefab != null)
            {
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity); // Instantiate the coin prefab at the given position
                Debug.Log($"Coin spawned at position: {spawnPosition}");
            }
            else
            {
                Debug.LogWarning("Coin prefab is not assigned!");
            }
        }
        else
        {
            Debug.LogWarning("Not enough coins to throw!");
        }
    }
}

