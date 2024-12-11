using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public bool isMoving = false;
    public GameObject player;

    public GameObject coin;
    public List<Unit> selectedUnits;
    public List<Vector3> targetPositionList;

    private int rand = 0;
    public bool coinBool = false;

    private void Awake()
    {
        // Initialize the selected units list
        selectedUnits = new List<Unit>();
    }

    void Start()
    {
        // Player initialization can be handled here if needed
    }

    void Update()
    {
        HandlePlayerReplacement();
        HandleUnitManagement();
        UpdateUnitPositions();
    }

    /// <summary>
    /// Handles replacing the main player if the current one is destroyed.
    /// </summary>
    private void HandlePlayerReplacement()
    {
        if (player == null && selectedUnits.Count > 0)
        {
            // Assign the first unit in the list as the new player
            player = selectedUnits[0].gameObject;
            selectedUnits.RemoveAt(0);

            var cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
            cameraFollow.target = player.transform;

            var playerComponent = player.GetComponent<MainPlayer>();
            playerComponent.enabled = true;

            player.GetComponent<MainPlayerHealth>().enabled = true;
            player.GetComponent<PlayerSupport>().enabled = false;
            player.GetComponent<Unit>().enabled = false;
            player.GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    /// <summary>
    /// Cleans up the selected units list to remove any destroyed units.
    /// </summary>
    private void HandleUnitManagement()
    {
        if (selectedUnits.Count > 0)
        {
            for (int i = selectedUnits.Count - 1; i >= 0; i--)
            {
                if (selectedUnits[i] == null)
                {
                    selectedUnits.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>
    /// Updates the positions and states of selected units relative to the player.
    /// </summary>
    private void UpdateUnitPositions()
    {
        if (player == null || selectedUnits.Count == 0) return;

        // Get positions around the player for units
        targetPositionList = GetPositionListAround(player.transform.position, 1f, selectedUnits.Count);

        int targetPositionIndex = 0;

        foreach (Unit unit in selectedUnits)
        {
            if (unit == null) continue;

            var agent = unit.gameObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.destination = targetPositionList[targetPositionIndex];

                // Check if the unit is in place
                bool inPlace = Vector3.Distance(unit.gameObject.transform.position, targetPositionList[targetPositionIndex]) < 0.1f;
                unit.gameObject.GetComponent<PlayerSupport>().inPlace = inPlace;
            }

            // Cycle through the target positions
            targetPositionIndex = (targetPositionIndex + 1) % targetPositionList.Count;
        }
    }

    /// <summary>
    /// Generates a list of positions around a specified point.
    /// </summary>
    /// <param name="startPosition">The central position.</param>
    /// <param name="distance">Distance from the center.</param>
    /// <param name="positionCount">Number of positions to generate.</param>
    /// <returns>List of positions.</returns>
    public List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyToRotation(new Vector3(1, 0, 1), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    /// <summary>
    /// Rotates a vector by a specified angle.
    /// </summary>
    /// <param name="vec">The vector to rotate.</param>
    /// <param name="angle">The rotation angle in degrees.</param>
    /// <returns>The rotated vector.</returns>
    public Vector3 ApplyToRotation(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, angle, 0) * vec;
    }

    /// <summary>
    /// Handles the coin spawning logic.
    /// </summary>
    public void CoinInst()
    {
        if (rand == 1)
        {
            coinBool = true;
            // Instantiate(coin, new Vector3(transform.position.x, 2f, transform.position.z), coin.transform.rotation);
        }
        else
        {
            coinBool = false;
        }

        rand = (rand + 1) % 6; // Reset rand to 0 after 5
    }
}
