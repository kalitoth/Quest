using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField]
    GameObject _inventory;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;

    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

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
