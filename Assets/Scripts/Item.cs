using UnityEngine;

[System.Serializable]
public class Item {
  public string itemName;
  public GameObject itemPrefab;
  public Sprite itemSprite;
  public int itemAmount;

  public Item(string name, GameObject prefab, Sprite sprite, int amount) {
    itemName = name;
    itemPrefab = prefab;
    itemSprite = sprite;
    itemAmount = amount;
  }
}