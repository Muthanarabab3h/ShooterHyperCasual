using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerHealth : MonoBehaviour
{
    public int health = 3;

    void Start()
    {
        health = Mathf.Clamp(PlayerPrefs.GetInt("Health", 1), 1, 5);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        Destroy(gameObject);
    }
}
