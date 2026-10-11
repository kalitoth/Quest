using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    int _hp;
    int _hpMax = 10;

    bool _alive = true;

    public int HP
    {
        get { return _hp; }
        set
        {
            value = Mathf.Clamp(value, 0, _hpMax);
            _hp = value;
        }
    }
    public int HPMax => _hpMax;
     
    public bool Alive { get { return _alive; } set { _alive = value; } }

    protected void Die()
    {
        if(HP<= 0)
        {
            _alive = false;
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
    public void TakeHeal(int heal)
    {
        HP += heal;
    }
  
}
