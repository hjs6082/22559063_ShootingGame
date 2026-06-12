using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InGameQuickSlotUI : MonoBehaviour
{
    [System.Serializable]
    public class QuickSlot
    {
        public Button button;
        public Image iconImage;
        public TMP_Text countText;
    }

    public QuickSlot[] slots = new QuickSlot[3];

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int slotIndex = i;
            slots[i].button.onClick.AddListener(() => OnClickSlot(slotIndex));
        }
        Refresh();
    }

    private void OnEnable()
    {
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnInventoryChanged += Refresh;
    }

    private void OnDisable()
    {
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnInventoryChanged -= Refresh;
    }

    public void Refresh()
    {
        if (PlayerInventory.Instance == null) return;

        var equipItems = PlayerInventory.Instance.equipItems;
        for (int i = 0; i < slots.Length; i++)
        {
            var item = i < equipItems.Count ? equipItems[i] : null;
            if (item != null && item.data != null)
            {
                slots[i].iconImage.sprite = item.data.icon;
                slots[i].iconImage.color = new Color(1f, 1f, 1f, 1f);
                slots[i].countText.text = item.count.ToString();
                slots[i].button.interactable = true;
            }
            else
            {
                slots[i].iconImage.sprite = null;
                slots[i].iconImage.color = new Color(1f, 1f, 1f, 0f);
                slots[i].countText.text = "";
                slots[i].button.interactable = false;
            }
        }
    }

    private void OnClickSlot(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);
        bool used = PlayerInventory.Instance.UseItem(PlayerInventory.Instance.equipItems, index);
        if (used) Refresh();
    }
}
