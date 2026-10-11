using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HPRecovery_master : MonoBehaviour
{
    [SerializeField]
    Player_master _player;

    [SerializeField]
    GameObject _image;

    Collider _playerCol;

    int num = 1;

    private void Start()
    { 
        _playerCol = _player.GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == _playerCol)
        { 
            _image.SetActive(true);
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == _playerCol)
        {
            _image.SetActive(false);
        }
        
    }

    

    private void Update()
    {
        if(_image == null)
        {
            return;
        }

        if (_image.activeSelf)
        { 
            _image.transform.rotation = _player.transform.rotation;


            if(Input.GetKeyDown(KeyCode.E))
            {
                HealingPotion_master.instance._potionAction.Invoke(num);
                Destroy(transform.parent.gameObject);
            }
        }
         

    }
}
