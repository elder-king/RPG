using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Collider2D col;
    public  Item ItemData;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(ItemData);
            Destroy(gameObject);
        }
    }
}
