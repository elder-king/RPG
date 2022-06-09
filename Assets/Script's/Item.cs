using UnityEngine;
[CreateAssetMenu(fileName ="New Item" , menuName = "Item/Create New Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public string ItemName;
    public enum ItemType { HealthPotion, ATKBoster, DEFBoster}
    public ItemType itemType;
    public int amount;
    public Sprite ItemSprite;
}

