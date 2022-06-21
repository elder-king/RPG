using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject menuPanel, Button;
    public Transform ItemContent;

    public bool isActive = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            switch (isActive)
            {
                case false:
                    menuPanel.gameObject.SetActive(true);
                    isActive = !isActive;
                    break;
                case true:
                    menuPanel.gameObject.SetActive(false);
                    isActive = !isActive;
                    break;
            }

    }
    public void ItemMenu()
    {
        foreach (Transform child in ItemContent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in Inventory.Instance.itemList)
        {
            GameObject ItemButton = Instantiate(Button) as GameObject;
            ItemButton.transform.Find("Text").GetComponent<Text>().text = item.ItemName;
            ItemButton.gameObject.GetComponent<ItemButtonScript>().item = item;
            ItemButton.transform.SetParent(ItemContent, false);
        }
    }
}
