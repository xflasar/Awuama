using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotPlacement : MonoBehaviour
{
    public GameObject plotPrefab;
    public Transform[] plotPositions;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlots();
    }

    private void CreatePlots() {
        foreach (Transform position in plotPositions) {
            Instantiate(plotPrefab, position.position, Quaternion.identity);
        }
    }
}
