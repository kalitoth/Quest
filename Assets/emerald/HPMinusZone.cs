using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMinusZone : MonoBehaviour
{
    [SerializeField]
    Player _player;
     
    float _time;
    float _interval = 1f;

    int damage = 1;

     List<Unit> _unit = new List<Unit>(10);
     
    private void OnTriggerEnter(Collider other)
    {
        Unit Unit = other.GetComponent<Unit>();
        if (Unit == null)
        {
            return;
            
        }

        if(!_unit.Contains(Unit))
        {
            _unit.Add(Unit);
        }
         
    }
    private void OnTriggerStay(Collider other)
    {
        _time += Time.deltaTime;

        if(_time < _interval)
        {
            return;
        }

        if(_unit.Count ==0)
        {
            return;
        } 

        for (int i = 0; i< _unit.Count; i++)
        {
            _unit[i].TakeDamage(damage);
        }
        _time = 0;

       
    }

    private void OnTriggerExit(Collider other)
    {
        
        Unit Unit = other.GetComponent<Unit>();
        if (Unit == null)
        {
            return;

        }
        _unit.Remove(Unit);
      
    }
}
