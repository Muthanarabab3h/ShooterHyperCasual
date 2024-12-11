using System;

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(InventoryItem item)
    {
        Item = item;
    }

    public InventoryItem Item { get; }
}
