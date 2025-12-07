using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthText : HealthIndidcatorsBase
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void ShowValue(float health, float maxHealth)
    {
        _text.text = $"{health.ToString()} / {maxHealth.ToString()}";
    }
}
