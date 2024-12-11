using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<GameObject> enemies; // List of enemies
    public List<Vector3> targetPositionList; // List of target positions for enemies
    public bool addingPlacesForEnemies = false;

    void Update()
    {
        if (enemies.Count != 0)
        {
            // Update target positions if there are enemies
            targetPositionList = GetPositionListAround(transform.position, 1f, enemies.Count);
        }
        else
        {
            // Clear target positions when no enemies
            targetPositionList.Clear();
        }
    }

    public List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();

        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount); // Calculate angle for each position
            Vector3 dir = ApplyToRotation(new Vector3(1, 0, 1), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }

        return positionList;
    }

    public Vector3 ApplyToRotation(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, angle, 0) * vec; // Rotate the vector by the specified angle
    }
}
