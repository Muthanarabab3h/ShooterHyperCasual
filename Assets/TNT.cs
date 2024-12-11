using UnityEngine;

public class TNT : MonoBehaviour, InventoryItem
{
    public string Name => "TNT";
    public Sprite Image { get; private set; }

    private void Start()
    {
        Image = Resources.Load<Sprite>("TNTSprite");
    }

    public void OnPickUp()
    {
        Debug.Log("TNT picked up.");
    }

    public void OnThrow(Vector3 position)
    {
        Instantiate(gameObject, position, Quaternion.identity);
        Destroy(gameObject, 5f);
        Debug.Log("TNT thrown.");
    }
}
