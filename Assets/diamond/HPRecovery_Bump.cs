using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRecovery_Bump : MonoBehaviour
{
    string _playerTag = "Player";

    int _heal = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _playerTag)
        {
            Player player = collision.gameObject.GetComponent<Player>();
             
            player.TakeHeal(_heal);

            Destroy(this.gameObject);
        }
    }
}
