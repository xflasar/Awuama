using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GridObjectPlacement : MonoBehaviour
{
    public Grid grid;
    public Camera mainCamera;
    public Tilemap tilemap;
    public GameObject plot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame){
            Vector3 worldPoint = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int cellPosition = tilemap.WorldToCell(worldPoint);
            if(tilemap.HasTile(cellPosition)){
                Debug.Log("Tile is there");
                GameObject objectToPlace = Instantiate(plot, cellPosition+ new Vector3(0.5f, 0.5f, 0), Quaternion.identity) as GameObject;
            } else {
                Debug.Log("Tile is not there");
            }
            Debug.Log(cellPosition);
        }
        
    }

    private void OnDrawGizmos() {
        
    }
}
