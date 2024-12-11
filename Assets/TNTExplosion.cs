using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTExplosion : MonoBehaviour
{
    public GameObject explosionEffect; // Assign the explosion GameObject in the Inspector
    public float explosionRadius = 5f; // The radius of the explosion
    public float delay = 1f; // Delay before the explosion occurs

    void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Instantiate the explosion effect at the TNT's position
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Find and destroy all enemies within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                Destroy(nearbyObject.gameObject);
            }
        }

        // Destroy the TNT object itself
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the explosion radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
