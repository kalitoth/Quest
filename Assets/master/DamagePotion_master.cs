using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePotion_master : Potion_master<DamagePotion_master>
{
    private void Start()
    {
        _potionName = "Damage";
        _potionAmount = -1;
    }
}
