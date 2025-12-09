using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarSmooth : HealthIndidcatorsBase
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _delayTime = 0.2f;

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

        _coroutine = StartCoroutine(ChangeValueSmoothly(health, maxHealth));
    }

    private IEnumerator ChangeValueSmoothly(float health, float maxHealth)
    {
        var wait = new WaitForSeconds(_delayTime);
        
        while (_slider.value != health) 
        {
            _slider.value = Mathf.MoveTowards(_slider.value, health, maxHealth);

            yield return wait;
        }
    }
}
