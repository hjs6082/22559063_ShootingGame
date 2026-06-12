using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public int bagSlotCount = 12;
    public int equipSlotCount = 3;

    public List<InventoryItem> bagItems = new List<InventoryItem>();
    public List<InventoryItem> equipItems = new List<InventoryItem>();

    [Header("시작 지급 아이템")]
    public ItemData[] startItems;

    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        bagItems.Clear();
        equipItems.Clear();
        for (int i = 0; i < bagSlotCount; i++) bagItems.Add(null);
        for (int i = 0; i < equipSlotCount; i++) equipItems.Add(null);

        if (startItems != null)
            foreach (var item in startItems)
                if (item != null) AddItem(item);
    }

    public bool AddItem(ItemData itemData, int count = 1)
    {
        if (itemData == null) return false;

        if (itemData.canStack)
        {
            // 장착 슬롯에 같은 아이템 있으면 우선 누적
            for (int i = 0; i < equipItems.Count; i++)
            {
                if (equipItems[i] != null && equipItems[i].data != null && equipItems[i].data == itemData && equipItems[i].count < itemData.maxStack)
                {
                    equipItems[i].count += count;
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }

            // 가방에 같은 아이템 있으면 누적
            for (int i = 0; i < bagItems.Count; i++)
            {
                if (bagItems[i] != null && bagItems[i].data != null && bagItems[i].data == itemData && bagItems[i].count < itemData.maxStack)
                {
                    bagItems[i].count += count;
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        for (int i = 0; i < bagItems.Count; i++)
        {
            if (bagItems[i] == null || bagItems[i].data == null)
            {
                bagItems[i] = new InventoryItem(itemData, count);
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        return false;
    }

    public void CleanupEquipSlots()
    {
        for (int i = 0; i < equipItems.Count; i++)
            if (equipItems[i] != null && equipItems[i].count <= 0)
                equipItems[i] = null;
        OnInventoryChanged?.Invoke();
    }

    public void MoveItem(List<InventoryItem> fromList, int fromIndex, List<InventoryItem> toList, int toIndex)
    {
        InventoryItem temp = fromList[fromIndex];
        fromList[fromIndex] = toList[toIndex];
        toList[toIndex] = temp;
        OnInventoryChanged?.Invoke();
    }

    public bool UseItem(List<InventoryItem> list, int index)
    {
        InventoryItem item = list[index];
        if (item == null || item.data.itemType != ItemType.Consumable) return false;

        switch (item.data.potionEffect)
        {
            case PotionEffect.HealHp:
                if (GameManager.Instance.Hp >= GameManager.Instance.maxHp)
                {
                    UIManager.Instance.ShowNotice("이미 최대 체력입니다!");
                    return false;
                }
                GameManager.Instance.TakeDamage(-item.data.healAmount);
                break;
            case PotionEffect.Invincible:
                if (!PlayerStatus.Instance.ApplyInvincible(item.data.effectDuration)) return false;
                break;
            case PotionEffect.ScoreDouble:
                if (!PlayerStatus.Instance.ApplyScoreDouble(item.data.effectDuration)) return false;
                break;
            default:
                return false;
        }

        item.count--;
        if (item.count <= 0 && list != equipItems) list[index] = null;
        OnInventoryChanged?.Invoke();
        return true;
    }
}
