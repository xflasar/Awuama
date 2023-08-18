using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CropSelectionUI : MonoBehaviour
{
    public CropFactory cropFactory;
    public Plot plot;
    public Button[] cropButtons;

    public Camera mainCamera;

    private string selectedCropType;

    public bool isShowing = false;

    private void Start()
    {
        gameObject.SetActive(false);
        // Attach button click listeners for crop selection
        for (int i = 0; i < cropButtons.Length; i++)
        {
            int buttonIndex = i; // Capture the current button index for the listener
            cropButtons[i].onClick.AddListener(() => SelectCrop(buttonIndex));
        }
    }

    private void Update()
    {
        if(isShowing){
            if (Mouse.current.leftButton.wasPressedThisFrame) {
                Ray ray = mainCamera.ScreenPointToRay(Pointer.current.position.ReadValue());
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll (ray, Mathf.Infinity);
                foreach (var hit in hits) {
                   if (hit.collider.tag == "Plot") {
                       plot = hit.collider.gameObject.GetComponent<Plot>();
                       if(!plot.IsCropPlanted())
                       {
                           plot.PlantCrop(cropFactory.GetCropTypeData(selectedCropType));
                           selectedCropType = null;
                           EventSystem.current.SetSelectedGameObject(null);
                       }
                   }
                }
            }
        }
    }

    public void ShowUI(bool state)
    {
        Debug.Log(state);
        isShowing = state;
        gameObject.SetActive(state);
    }
    public void SelectCrop(int cropIndex)
    {
        selectedCropType = cropFactory.cropTypes[cropIndex].cropName;
    }
}
