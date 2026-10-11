using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NPC_master : MonoBehaviour
{

    [SerializeField]
    GameObject _store;
    [SerializeField]
    GameObject _pressE;

    string _playerTag = "Player";
    bool _isStore = false;

    Transform _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _playerTag)
        {
            _player = other.transform;

            _isStore = true;
            if(!_pressE.activeSelf)
            {
                _pressE.SetActive(true);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == _playerTag)
        {
            _player = null;

            _isStore = false;
            
            if (_pressE.activeSelf)
            {
                _pressE.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(_isStore)
        {
            if( _player != null )
            {
                _pressE.transform.rotation = _player.transform.rotation;
            }
             
            if (Input.GetKeyDown(KeyCode.E))
            {
                bool active = _store.activeSelf ? false : true;

                _store.SetActive(active);
            }
        }
        else
        {
            _store.SetActive(false);
        }


         
    }
}
