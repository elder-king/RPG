using UnityEngine;

public class ItemButtonScript : MonoBehaviour
{
    public Item item;

    public void OnClick()
    {
        GameObject.Find("Battel System").GetComponent<BattelSystem>().Input4(item);
        Destroy(gameObject);
    }
}
