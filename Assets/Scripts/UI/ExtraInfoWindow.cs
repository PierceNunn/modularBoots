using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExtraInfoWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _infoText;
    public void UpdateInfoDisplay(string newInfo)
    {
        _infoText.text = newInfo;
    }
}
