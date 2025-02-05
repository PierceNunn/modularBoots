using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;

public class ModDisplayController : MonoBehaviour
{
    [SerializeField] private GenericMod _modToDisplay;
    [SerializeField] private bool _showAllInfo = true;
    [SerializeField] private Image _modIconDisplay;
    [SerializeField] private TextMeshProUGUI _modNameDisplay;
    [SerializeField] private TextMeshProUGUI _modDescriptionDisplay;
    [SerializeField] private PlayerInput pi;

    private Rect bounds;
    private RectTransform rectTransform;

    public GenericMod ModToDisplay { get => _modToDisplay; set => _modToDisplay = value; }

    public void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        bounds = rectTransform.rect;

    }

    public void LateUpdate()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(mousePos);
        if (rectTransform.rect.Contains(localMousePosition) && _modToDisplay != null)
        {
            try
            {
                //FindObjectOfType<ExtraInfoWindow>().gameObject.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
                FindObjectOfType<ExtraInfoWindow>().UpdateInfoDisplay(_modToDisplay.DetailedInfoString());
            }
            catch
            {
                Debug.LogWarning("ExtraInfoWindow either doesn't exist or is missing a RectTransform");
            }
            
        }
    }
    public void UpdateDisplayInfo()
    {
        if(_modToDisplay != null)
        {
            _modIconDisplay.sprite = ModToDisplay.ModIcon;
            _modNameDisplay.text = ModToDisplay.ModName;

            if (_showAllInfo)
                _modDescriptionDisplay.text = ModToDisplay.ModDescription;
        }
        else
        {
            _modIconDisplay.sprite = null;
            _modNameDisplay.text = "";

            if (_showAllInfo)
                _modDescriptionDisplay.text = "";
        }
        
    }

    public void UpdateDisplayInfo(GenericMod mtd)
    {
        _modToDisplay = mtd;
        UpdateDisplayInfo();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        print("glort");
    }
}
