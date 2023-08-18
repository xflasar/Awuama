using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class CropType
{
    public string cropName;
    public GameObject seedPrefab;
    public float growthTime;
}

public class WaterBucket {
  public float waterAmount;
}

public class InventoryManager : MonoBehaviour
{
    public List<CropType> availableCrops = new List<CropType>();
    public List<CropType> playerInventory = new List<CropType>();
    public WaterBucket _waterBucket = new WaterBucket();


    public List<CropType> GetPlayerInventory()
    {
        return playerInventory;
    }

    public void UseCrop(CropType cropType)
    {
        playerInventory.Remove(cropType);
        // Update UI or other relevant aspects
    }

    public void AddCrop(CropType cropType)
    {
        playerInventory.Add(cropType);
        // Update UI or other relevant aspects
    }

    public float GetWaterAmount()
    {
      return _waterBucket.waterAmount;
        // Check if the player has a water bucket in their inventory
    }

    public void RemoveWater(float amount)
    {
      if(_waterBucket.waterAmount < amount)
        return;
      _waterBucket.waterAmount -= amount;
    }

    public void Update() {
      // Debug.Log(_waterBucket.waterAmount);
      if(_waterBucket.waterAmount < 1.0f)
      _waterBucket.waterAmount += Time.deltaTime;
    }
}
