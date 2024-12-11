using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Image[] itemSlots;

    private void Start()
    {
        inventory.ItemAdded += OnItemAdded;
        inventory.ItemRemoved += OnItemRemoved;
    }

    private void OnItemAdded(object sender, InventoryEventArgs e)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].sprite == null)
            {
                itemSlots[i].sprite = e.Item.Image;
                itemSlots[i].enabled = true;
                break;
            }
        }
    }

    private void OnItemRemoved(object sender, InventoryEventArgs e)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].sprite == e.Item.Image)
            {
                itemSlots[i].sprite = null;
                itemSlots[i].enabled = false;
                break;
            }
        }
    }
}
