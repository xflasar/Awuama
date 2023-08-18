using System.Collections.Generic;
using UnityEngine;

public class CropFactory : MonoBehaviour
{
    public List<CropTypeData> cropTypes = new List<CropTypeData>();

    public void AddCropType(CropTypeData cropType)
    {
        cropTypes.Add(cropType);
    }

    public CropTypeData GetCropTypeData(string cropName)
    {
        return cropTypes.Find(cropType => cropType.cropName == cropName);
    }
}
