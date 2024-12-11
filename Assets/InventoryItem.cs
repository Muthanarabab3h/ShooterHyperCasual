using UnityEngine;

public interface InventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickUp();
    void OnThrow(Vector3 position);
}
