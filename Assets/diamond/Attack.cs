using System.Collections;
using System.Collections.Generic; 
using System.Threading;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Attack : MonoBehaviour
{
    string _monsterTag = "Monster";

    bool _oneAttack = true;

    int _damage = 1;

    ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
   {
       if(!_oneAttack)
       { 
           return;
       }
   
       if(other.tag == _monsterTag)
       {
           Monster monster = other.GetComponent<Monster>();

            if(monster == null)
            {
                Debug.Log("몬스터가 아니다");
                return;
            }
   
           monster.TakeDamage(_damage);
   
           Debug.Log(monster.HP);
   
           _oneAttack = false;

            _particle.transform.position = other.ClosestPoint(transform.position);

             
            _particle.Play();

           Debug.Log("Attack");
       }
          
       
   }
}
