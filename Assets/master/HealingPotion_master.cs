using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion_master : Potion_master<HealingPotion_master>
{
    private void Start()
    {
        _potionName = "Heal";
        _potionAmount = 1;
    }

}
