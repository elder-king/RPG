using UnityEngine;
using UnityEngine.UI;
public class IUseItem : MonoBehaviour
{
    inBattleScript PLayerInBattle;
    Movement Player;
    characterStats stats;
    Item item;
    Inventory inventory;
    bool ItemUsed = true;
    public void Use()
    {

        if (GameObject.Find("Battel System") != null)
        {
            item = BattelSystem.Instance.ChosinItem;
            PLayerInBattle = GetComponent<inBattleScript>();
        }
        else
        {
            Player = GetComponent<Movement>();
        }

        stats = GetComponent<characterStats>();
        inventory = Inventory.Instance;
        string ItemName = item.name;

        switch (ItemName)
        {
            case "Health Potion":
                if (stats.Hp < stats.MaxHp)
                {
                    Notefaction.Instance.Notefy("doset work?", () => { PLayerInBattle.OnHealButton(20); Notefaction.Instance.Hide();}, () => { Notefaction.Instance.Hide();});
                }
                else
                {
                    ItemUsed = false;
                }
                break;

            case "ATK Boster":
                stats.Damage += 10;
                break;

            case "DEF Boster":
                stats.Defince += 10;
                Debug.Log(stats.unitName + " Defunce Incresed " + 10);
                break;
            case "Flash Light":
                Player.FlashLight();
                break;
        }

        SwitchTurn();
    }
    void SwitchTurn()
    {
        if (PLayerInBattle != null && ItemUsed)
        {
            BattelSystem.Instance.ActCount();
            if (item.Limited)
            {
                inventory.RemoveItame(item);
        
                PLayerInBattle.GetComponent<inBattleScript>().EndTurn();
            }
        }
        ItemUsed = true;
    }
    public void GetItem(Item Reitem)
    {
        item = Reitem;
        Use();
    }
}