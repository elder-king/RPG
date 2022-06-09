using UnityEngine;
public class IUseItem : MonoBehaviour
{
    public void Use()
    {
        inBattleScript PLayer = GetComponent<inBattleScript>();
        characterStats stats = GetComponent<characterStats>();
        Item bsItem = GetComponent<inBattleScript>().BS.ChosinItem;
        Inventory inventory = GetComponent<inBattleScript>().BS.inventory;

        switch (bsItem.itemType)
        {
            case Item.ItemType.HealthPotion:
                PLayer.OnHealButton(20);
                break;

            case Item.ItemType.ATKBoster:
                stats.Damage += 10;
                break;

            case Item.ItemType.DEFBoster:
                stats.Defince += 10;
                Debug.Log(stats.unitName + " Defunce Incresed " + 10);
                break;
        }
        inventory.RemoveItame(bsItem);
    }
}