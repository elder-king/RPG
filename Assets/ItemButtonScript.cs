using UnityEngine;


public class ItemButtonScript : MonoBehaviour
{
    public Item item;

    public void OnClick()
    {
        if (GameObject.Find("Battel System") != null)
        {
            BattelSystem.Instance.Input4(item);
        }
        else
        {
            IUseItem Player = GameObject.Find("player").GetComponent<IUseItem>();
            if (item.itemType != Item.ItemType.KeyItem)
            {
                if (item.UsebleOutSideBattle == true || item.equippable)
                {
                    Player.GetItem(item);
                    Debug.Log(item.ItemName + " has ben used");
                }
                else
                {
                    Debug.Log(item.ItemName + " Can't be used Outside the Battle");
                    return;
                }
            }
            else
            {
                Debug.Log(" Can't use This Item ");
                return;
            }
            if (item.Limited == true)
            {
                if (item.itemType != Item.ItemType.Tool)
                {
                    Inventory.Instance.RemoveItame(item);
                    Destroy(gameObject);
                }
            }
        }
    }
    public void GetIteam()
    {
        Desctription.Instance.OnEnter(item.Description);
    }
}
