using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public CropSelectionUI cropSelectionUI;
    public InventoryManager inventoryManager;

    public Camera mainCamera;

    private float gameTimer = 0f;

    public InputAction _showCropSelection;

    public GameObject selectedPlot;

    private void OnEnable()
    {
      _showCropSelection.Enable();
      _showCropSelection.started += ctx => cropSelectionUI.ShowUI(!cropSelectionUI.isShowing);
    }

    private void OnDisable() {
      _showCropSelection.Disable();
    }

    private void Start()
    {
        // Initialize game components and setup
    }

    private void Update()
    { 
      CheckForPlotSelected();
      // Update game timer or other global logic
      gameTimer += Time.deltaTime;
    }

    public float GetGameTimer()
    {
        return gameTimer;
    }


    // TODO:
    // - Fix problem of UnassignedReferenceException: The variable selectedPlot of GameManager has not been assigned.
    private void CheckForPlotSelected() {
      if (Mouse.current.leftButton.wasPressedThisFrame && !cropSelectionUI.isShowing) {
        Ray ray = mainCamera.ScreenPointToRay(Pointer.current.position.ReadValue());
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll (ray, Mathf.Infinity);
        foreach (var hit in hits) {
               Debug.Log(hit.collider.name);
          if(hit.collider.tag == "PlotUI") return;
          if (hit.collider.tag == "Plot" && hit.collider.gameObject != selectedPlot) {
             if(selectedPlot)
             selectedPlot.GetComponent<Plot>()._plotUI.SetActive(false);
             
             selectedPlot = hit.collider.gameObject;
             EventSystem.current.SetSelectedGameObject(selectedPlot);
             selectedPlot.GetComponent<Plot>()._plotUI.SetActive(true);
          } else if(hit.collider.gameObject != selectedPlot) {
             selectedPlot?.GetComponent<Plot>()._plotUI.SetActive(false);
             EventSystem.current.SetSelectedGameObject(null);
          }
        }
        if(hits.Length == 0) {
          selectedPlot?.GetComponent<Plot>()._plotUI.SetActive(false);
          selectedPlot = null;
          EventSystem.current.SetSelectedGameObject(null);
        }
      }
    }
}
