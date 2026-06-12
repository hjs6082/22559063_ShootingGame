using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public Image iconImage;
    public TMP_Text countText;

    private InventoryUI inventoryUI;
    private List<InventoryItem> itemList;
    private int index;

    public void SetSlot(InventoryUI inventoryUI, List<InventoryItem> itemList, int index)
    {
        this.inventoryUI = inventoryUI;
        this.itemList = itemList;
        this.index = index;
        // 현재 슬롯의 아이콘과 개수 텍스트 갱신
        InventoryItem item = itemList[index];
        if (item != null && item.data != null)
        {
            iconImage.sprite = item.data.icon;
            iconImage.color = new Color(1f, 1f, 1f, 1f);
            countText.text = item.count > 1 ? item.count.ToString() : "";
        }
        else
        {
            iconImage.sprite = null;
            iconImage.color = new Color(1f, 1f, 1f, 0f);
            countText.text = "";
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemList[index] == null || itemList[index].data == null) return;
        if (inventoryUI.dragIcon == null) return;

        inventoryUI.dragIcon.sprite = itemList[index].data.icon;
        inventoryUI.dragIcon.color = new Color(1f, 1f, 1f, 0.8f);
        inventoryUI.dragIcon.gameObject.SetActive(true);
        inventoryUI.dragIcon.raycastTarget = false;
        MoveDragIcon(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventoryUI.dragIcon == null || !inventoryUI.dragIcon.gameObject.activeSelf) return;
        MoveDragIcon(eventData);
    }

    private void MoveDragIcon(PointerEventData eventData)
    {
        RectTransform canvasRect = inventoryUI.GetComponent<RectTransform>();
        Canvas canvas = inventoryUI.GetComponentInParent<Canvas>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, eventData.position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint);
        inventoryUI.dragIcon.rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inventoryUI.dragIcon == null) return;
        inventoryUI.dragIcon.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 시작 슬롯을 찾아 PlayerInventory.MoveItem() 호출
        InventorySlotUI fromSlot = eventData.pointerDrag.GetComponent<InventorySlotUI>();
        if (fromSlot == null) return;
        PlayerInventory.Instance.MoveItem(fromSlot.itemList, fromSlot.index, itemList, index);
        inventoryUI.Refresh();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemList[index] == null || itemList[index].data == null) return;
        if (string.IsNullOrEmpty(itemList[index].data.itemInfo)) return;
        inventoryUI.ShowTooltip(itemList[index].data.itemInfo);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        inventoryUI.MoveTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryUI.HideTooltip();
    }
}
