using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantUi : MonoBehaviour
{
    public GameObject cameraMain;
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = GameObject.Find("Main Camera");
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Canvas>().worldCamera = cameraMain.GetComponent<Camera>();
    }
}
