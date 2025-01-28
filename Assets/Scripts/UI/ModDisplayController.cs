using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModDisplayController : MonoBehaviour
{
    [SerializeField] private GenericMod _modToDisplay;
    [SerializeField] private bool _showAllInfo = true;
    [SerializeField] private Image _modIconDisplay;
    [SerializeField] private TextMeshProUGUI _modNameDisplay;
    [SerializeField] private TextMeshProUGUI _modDescriptionDisplay;

    public GenericMod ModToDisplay { get => _modToDisplay; set => _modToDisplay = value; }

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
}
