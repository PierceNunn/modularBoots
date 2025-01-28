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

    public void UpdateDisplayInfo()
    {
        _modIconDisplay.sprite = _modToDisplay.ModIcon;
        _modNameDisplay.text = _modToDisplay.ModName;
        _modDescriptionDisplay.text = _modToDisplay.ModDescription;
    }
}
