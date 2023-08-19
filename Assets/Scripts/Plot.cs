using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Plot : MonoBehaviour
{
    public Transform cropSpawnPosition; // Where the crop prefab will be instantiated
    public GameObject _plotUI;
    public GameObject _InventoryManager;

    // Private vars
    public float _waterAmount = 0.0f;
    private GameObject plantedCropInstance; // Instance of the planted crop

    private void Start()
    {
      _InventoryManager = GameObject.Find("InventoryManager");
    }

    public void Update()
    {
      _waterAmount -= Time.deltaTime * 0.1f;
      if (_waterAmount < 0.0f)
      {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("1 Tiles/Farming/DirtDry");
        _waterAmount = 0.0f;
      } else {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("1 Tiles/Farming/DirtWet");
      }
    }

    public void PlantCrop(CropTypeData cropTypeData)
    {
        if (plantedCropInstance == null && cropTypeData != null)
        {
            plantedCropInstance = Instantiate(cropTypeData.seedPrefab, cropSpawnPosition.position, Quaternion.identity);
            plantedCropInstance.transform.SetParent(cropSpawnPosition);
            Crop cropComponent = plantedCropInstance.GetComponent<Crop>();
            if (cropComponent != null)
            {
              cropComponent.Initialize(cropTypeData);
              cropComponent.StartGrowing();
            }
        }
    }
    
    public void RemovePlantedCrop(){
      plantedCropInstance = null;
    }

    public bool IsCropPlanted()
    {
        return plantedCropInstance != null;
    }

    public void HarvestCrop()
    {
      plantedCropInstance.GetComponent<Crop>().Harvest();
      plantedCropInstance = null;
    }

    public void RemoveCrop() {
      Destroy(plantedCropInstance);
      plantedCropInstance = null;
    }

    public void AddWater()
    {
      Debug.Log("Called Add Water");
      float amount; 
      if(_InventoryManager.GetComponent<InventoryManager>().GetWaterAmount() < 0.5f){
        amount = _InventoryManager.GetComponent<InventoryManager>().GetWaterAmount();
      } else {
        amount = 0.5f;
      }
      _InventoryManager.GetComponent<InventoryManager>().RemoveWater(amount);
      _waterAmount += amount;
    }

    public void RemovePlot() {
      Destroy(gameObject);
    }
}
