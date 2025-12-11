using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker
{
    void TakeDamage(int damage);
    void DealDamage(IAttacker defender);
    void Die();
}
