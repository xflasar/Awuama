using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropTypeData _cropTypeData;
    public bool isFullyGrown = false;
    public bool isDried = false;
    public float currentWaterLevel = 0.0f;
    public float currentGrowthTime = 0.0f;
    public float witherTimeLeft = 0.0f;

    public float growthPercentage = 0.0f;
    public int _growthStage = 0;

    public void Initialize(CropTypeData cropTypeData)
    {
        this._cropTypeData = cropTypeData;
        Debug.Log("Crop initialized: " + gameObject.transform.parent.transform.position);
    }

    public void StartGrowing()
    {
      witherTimeLeft = _cropTypeData.dryingTime;

    }

    private void Wither() {
      witherTimeLeft -= Time.deltaTime;
      if(witherTimeLeft <= 0.0f) {
        witherTimeLeft = 0.0f;
        isDried = true;
        isFullyGrown = false;
      }
    }

    public void Update()
    {
      if(isFullyGrown) {
        Wither();
      } else if(isDried) {
        gameObject.GetComponentInParent<Plot>().RemovePlantedCrop();
        Destroy(gameObject);
      } else {
        currentGrowthTime += Time.deltaTime;
        growthPercentage = currentGrowthTime / _cropTypeData.growthTime;
        if(0.5f <= growthPercentage && growthPercentage <= 1.0f) {
          _growthStage = 1;
        } else if(growthPercentage >= 1.0f) {
          growthPercentage = 1.0f;
          _growthStage = 2;
          isFullyGrown = true;
        }
      }

      switch(_growthStage) {
        case 0:
          gameObject.transform.Find("Seeded").gameObject.SetActive(true);
        break;
        case 1:
          gameObject.transform.Find("Seeded").gameObject.SetActive(false);
          gameObject.transform.Find("Growing").gameObject.SetActive(true);
          currentWaterLevel = _cropTypeData.waterNeededPerSecond * 1.5f;
          break;
        case 2:
          gameObject.transform.Find("Growing").gameObject.SetActive(false);
          gameObject.transform.Find("Developed").gameObject.SetActive(true);
          break;
      }
    }

    public int Harvest() {
      int yield = _cropTypeData.yield;
      Destroy(gameObject);
      return yield;
    }
}