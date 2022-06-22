using UnityEngine;
[CreateAssetMenu(fileName ="New Item" , menuName = "Item/Create New Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public string ItemName;
    public string Description;
    public enum ItemType { HealthPotion, ATKBoster, DEFBoster, KeyItem, Tool}
    public ItemType itemType;
    public int Amount;
    public Sprite ItemSprite;
    public bool Limited = true;
    public bool equippable = false;
    public bool UsebleOutSideBattle;
    public bool UsebleInSideBattle;
}

