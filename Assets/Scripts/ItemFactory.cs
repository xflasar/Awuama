using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
      items.Add(item);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }
}
