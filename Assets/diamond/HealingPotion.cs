using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : Potion<HealingPotion>
{
    private void Start()
    {
        _potionName = "Heal";
        _potionAmount = 1;
    }

}
