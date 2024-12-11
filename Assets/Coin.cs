using UnityEngine;

public class Coin : MonoBehaviour, InventoryItem
{
    public string Name => "coin";
    public Sprite Image { get; private set; }

    private void Start()
    {
        Image = Resources.Load<Sprite>("CoinSprite");
    }

    public void OnPickUp()
    {
        Debug.Log("Coin picked up.");
    }

    public void OnThrow(Vector3 position)
    {
        Instantiate(gameObject, position, Quaternion.identity);
        Debug.Log("Coin thrown.");
    }
}
