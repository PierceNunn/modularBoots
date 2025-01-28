using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModDisplayController : MonoBehaviour
{
    [SerializeField] private GenericMod _modToDisplay;
    [SerializeField] private Image _modIconDisplay;
    [SerializeField] private TextMeshProUGUI _modNameDisplay;
    [SerializeField] private TextMeshProUGUI _modDescriptionDisplay;

    public GenericMod ModToDisplay { get => _modToDisplay; set => _modToDisplay = value; }

    public void UpdateDisplayInfo()
    {
        _modIconDisplay.sprite = ModToDisplay.ModIcon;
        _modNameDisplay.text = ModToDisplay.ModName;
        _modDescriptionDisplay.text = ModToDisplay.ModDescription;
    }

    public void UpdateDisplayInfo(GenericMod mtd)
    {
        _modToDisplay = mtd;
        UpdateDisplayInfo();
    }
}
