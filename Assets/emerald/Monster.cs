using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
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
