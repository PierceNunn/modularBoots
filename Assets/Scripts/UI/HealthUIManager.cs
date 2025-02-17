using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform _healthBarMask;
    [SerializeField] private float _healthBarFullWidth;

    private float healthBarHeight;
    private HealthSystem hs;
    // Start is called before the first frame update
    void Start()
    {
        hs = FindObjectOfType<HealthSystem>();
        healthBarHeight = _healthBarMask.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = hs.CurrentHealth / hs.MaxHealth;
        _healthBarMask.sizeDelta = new Vector2(healthPercent * _healthBarFullWidth, healthBarHeight);
    }
}
