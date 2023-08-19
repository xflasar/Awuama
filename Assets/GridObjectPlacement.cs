using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[System.Serializable]
public class Slot {
    public int slotIndex;
    public string itemName;
    public GameObject itemPrefab;
    public Sprite itemSprite;
    public int itemAmount;

    public Slot(int index){
        slotIndex = index;
        itemName = "";
        itemPrefab = null;
        itemSprite = null;
        itemAmount = 0;
    }
}
[System.Serializable]
public class GridObjectPlacement : MonoBehaviour
{
    public ItemFactory itemFactory;
    public Grid grid;
    public Camera mainCamera;
    public Tilemap tilemap;
    public GameObject plot;
    public Vector3Int cellPosition;
    public List<Slot> slots;

    public Canvas canvas;
    public Color rectangleColor = Color.green;
    public bool isCreatingRectangle = false;
    public Vector2 initialMousePosition;

    public GameObject rectangle;

    public Slot selectedSlot;
    public List<GameObject> buttons;

    // Start is called before the first frame update
    void Start()
    {
        CreateRectangle();
        for(int i = 0; i < 12; i++){
            GameObject button = GameObject.Find("Slot"+(i+1));
            buttons.Add(button);
            slots.Add(new Slot(i));
        }
        AssignItemToSlot(itemFactory.GetItem("Farming Plot"), 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        cellPosition = tilemap.WorldToCell(worldPoint);
        rectangle.SetActive(true);
        rectangle.transform.position = cellPosition + new Vector3(0.5f, 0.5f, 0);
        if(Mouse.current.leftButton.wasPressedThisFrame){
            if(tilemap.HasTile(cellPosition)){
                Debug.Log("Tile is there");
                GameObject objectToPlace = Instantiate(selectedSlot.itemPrefab, cellPosition+ new Vector3(0.5f, 0.5f, 0), Quaternion.identity) as GameObject;
            } else {
                Debug.Log("Tile is not there");
            }
            Debug.Log(cellPosition);
        }
        
    }

    public void SelectObjectSlot(int index) {
        selectedSlot = slots[index];
    }

    public void AssignItemToSlot(Item item, int slotIndex) {
        buttons[slotIndex].GetComponent<Image>().sprite = item.itemSprite;
        slots[slotIndex].itemAmount = item.itemAmount;
        slots[slotIndex].itemName = item.itemName;
        slots[slotIndex].itemPrefab = item.itemPrefab;
        slots[slotIndex].itemSprite = item.itemSprite;
    }

    private void CreateRectangle(){
        rectangle = new GameObject("Rectangle", typeof(RectTransform), typeof(Image));
        RectTransform rectTransform = rectangle.GetComponent<RectTransform>();
        Image image = rectangle.GetComponent<Image>();

        rectTransform.SetParent(canvas.transform, false);
        rectTransform.rect.Set(0, 0, 0.7f, 0.7f);
        rectTransform.localScale = new Vector3(0.7f, 0.7f, 1);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;

        image.sprite = Resources.Load<Sprite>("Other/UI/GridObjectPlacementRectangle");
        image.raycastTarget = false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(cellPosition + new Vector3(0.5f, 0.5f, 0), Vector3.one);
        Gizmos.color = Color.red;
        tilemap.GetTile(cellPosition);
    }
}
