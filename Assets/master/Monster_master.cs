using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_master : Unit_master
{
     
     
    private void Awake()
    {
        HP = HPMax;
    }
     
    
    private void Update()
    {
        Die();
        if(!Alive)
        {
            Destroy(this.gameObject);
        }
    }
}
