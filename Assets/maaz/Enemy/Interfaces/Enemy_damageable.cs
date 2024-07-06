using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy_damageable
{   
    void Damage(float damage);

    void Death();

    float MaxHealth {get;set;}

    float CurrentHealth {  get; set; }
}
