using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarSmooth : HealthIndidcatorsBase
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed;

    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void OnEnable()
    {
        _slider.maxValue = Health.MaxValue;
        _slider.minValue = Health.MinValue;
        
        base.OnEnable();
    }

    protected override void ShowValue(float health, float maxHealth)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValueSmoothly(health));
    }

    private IEnumerator ChangeValueSmoothly(float health)
    {
        while (_slider.value != health) 
        {
            _slider.value = Mathf.MoveTowards(_slider.value, health, _smoothSpeed);

            yield return null;
        }
    }
}
