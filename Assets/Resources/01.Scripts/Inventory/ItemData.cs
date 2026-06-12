using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    Program,
    Etc
}

public enum PotionEffect
{
    None,
    HealHp,
    Invincible,
    ScoreDouble
}

[CreateAssetMenu(fileName = "Item_", menuName = "ShootingGame/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string itemName;
    public ItemType itemType;
    public Sprite icon;
    public int attackBonus;
    public int defenseBonus;
    public bool canStack = true;
    public int maxStack = 99;

    [Header("Info")]
    [TextArea] public string itemInfo;

    [Header("Potion")]
    public PotionEffect potionEffect = PotionEffect.None;
    public int healAmount = 1;
    public float effectDuration = 5f;
}
