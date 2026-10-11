using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Inventory_master : MonoBehaviour
{
    
    [SerializeField]
    GameObject _inventory;
     
    private void Update()
    {
        playerInventory(); 
    }

    void playerInventory()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_inventory.activeSelf)
            {
                _inventory.SetActive(false);
            }
            else
            {
                _inventory.SetActive(true);
            }
               
        }

    }

}
