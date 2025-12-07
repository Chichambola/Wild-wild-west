using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : HealthIndidcatorsBase
{
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void ShowValue(float health, float maxHealth)
    {
       _slider.value = health;
       _slider.maxValue = maxHealth;
    }
}
