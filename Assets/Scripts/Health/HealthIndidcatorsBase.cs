using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthIndidcatorsBase : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected void Start()
    {
        ShowValue(Health.Value, Health.MaxValue);
    }

    protected virtual void OnEnable()
    {
        Health.ValueChanged += ShowValue;
    }

    protected void OnDisable()
    {
        Health.ValueChanged -= ShowValue;
    }

    protected abstract void ShowValue(float health, float maxHealth);
}
