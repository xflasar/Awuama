using UnityEngine;

[System.Serializable]
public class CropTypeData
{
    public string cropName;
    public GameObject seedPrefab;
    public float growthTime;
    public float dryingTime;
    public int yield;
    public float waterNeededPerSecond;
}
