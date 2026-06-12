using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DropItem : MonoBehaviour
{
    public ItemData itemData;
    public int count = 1;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public void Setup(ItemData data, int itemCount = 1)
    {
        itemData = data;
        count = itemCount;
        if (spriteRenderer != null && data.icon != null)
            spriteRenderer.sprite = data.icon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInventory.Instance.AddItem(itemData, count);
            Destroy(this.gameObject);
        }
    }
}
