using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public GameObject loseMenu; // The lose menu UI
    public int coins = 0; // Player's coin count
    public bool guard = false; // Guard status
    public bool doubleCharacter = false; // Double character status, if applicable

    public TextMeshProUGUI coinText; // UI element to display coins
    public TextMeshProUGUI healthText; // UI element to display health

    public int playerHealth = 3; // Default player health

    void Start()
    {
        if (loseMenu != null)
        {
            loseMenu.SetActive(false); // Ensure the lose menu is hidden at the start
        }

        UpdateCoinUI();
        UpdateHealthUI(playerHealth); // Set initial health display
    }

    /// <summary>
    /// Adds coins to the player's total and updates the UI.
    /// </summary>
    /// <param name="amount">The number of coins to add.</param>
    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    /// <summary>
    /// Updates the coin count UI.
    /// </summary>
    public void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coins.ToString();
        }
    }

    /// <summary>
    /// Updates the health UI and handles player death.
    /// </summary>
    /// <param name="healthChange">Change in health (negative for damage).</param>
    public void UpdateHealth(int healthChange)
    {
        if (guard) return; // Ignore damage when guard is active

        playerHealth += healthChange;
        UpdateHealthUI(playerHealth);

        if (playerHealth <= 0)
        {
            TriggerLoseMenu();
        }
    }

    /// <summary>
    /// Updates the health UI text.
    /// </summary>
    /// <param name="health">The player's current health.</param>
    public void UpdateHealthUI(int health)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + Mathf.Max(0, health).ToString();
        }
    }

    /// <summary>
    /// Activates the lose menu and stops the game.
    /// </summary>
    public void TriggerLoseMenu()
    {
        if (loseMenu != null)
        {
            loseMenu.SetActive(true);
        }
        Time.timeScale = 0; // Pause the game
        Debug.Log("Game Over! Lose Menu Activated.");
    }

    /// <summary>
    /// Restarts the game by reloading the current scene.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
