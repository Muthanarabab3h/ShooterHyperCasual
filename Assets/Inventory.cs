using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 2;
    private List<InventoryItem> mItems = new List<InventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public void AddItem(InventoryItem item)
    {
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null && collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickUp();

                ItemAdded?.Invoke(this, new InventoryEventArgs(item));
            }
        }
    }

    public void RemoveItem(InventoryItem item, Vector3 throwPosition)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnThrow(throwPosition);

            ItemRemoved?.Invoke(this, new InventoryEventArgs(item));
        }
    }
}
