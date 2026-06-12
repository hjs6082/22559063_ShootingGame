using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public InventorySlotUI[] bagSlots;
    public InventorySlotUI[] equipSlots;
    public Image dragIcon;

    [Header("Tooltip")]
    public GameObject tooltipPanel;
    public TMP_Text tooltipText;

    private void Start()
    {
        inventoryPanel.SetActive(false);
        if (dragIcon != null) dragIcon.gameObject.SetActive(false);
        if (tooltipPanel != null) tooltipPanel.SetActive(false);
    }

    public void ShowTooltip(string info)
    {
        if (tooltipPanel == null) return;
        tooltipText.text = info;
        // 툴팁이 마우스 이벤트 가로채지 않도록
        foreach (var graphic in tooltipPanel.GetComponentsInChildren<UnityEngine.UI.Graphic>())
            graphic.raycastTarget = false;
        tooltipPanel.SetActive(true);
        MoveTooltip();
    }

    public void MoveTooltip()
    {
        if (tooltipPanel == null || !tooltipPanel.activeSelf) return;
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(), Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint);
        tooltipPanel.GetComponent<RectTransform>().localPosition = localPoint + new Vector2(10f, -10f);
    }

    public void HideTooltip()
    {
        if (tooltipPanel == null) return;
        tooltipPanel.SetActive(false);
    }

    public void Toggle()
    {
        // 패널 열기/닫기
        // 열릴 때 Refresh() 호출
        bool isOpen = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isOpen);
        if (isOpen)
        {
            PlayerInventory.Instance?.CleanupEquipSlots();
            Refresh();
        }
    }

    public void Refresh()
    {
        if (PlayerInventory.Instance == null) return;

        for (int i = 0; i < bagSlots.Length; i++)
            bagSlots[i].SetSlot(this, PlayerInventory.Instance.bagItems, i);
        for (int i = 0; i < equipSlots.Length; i++)
            equipSlots[i].SetSlot(this, PlayerInventory.Instance.equipItems, i);
    }
}
