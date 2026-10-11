using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gold_master : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _gold;

    public int _goldNum = 3000;
    public Action _goldAction;

    public static Gold_master instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;

        _goldAction += GoldRefresh;
        _goldAction.Invoke();
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
    void GoldRefresh()
    {
        _gold.text = $"Gold : {_goldNum}";
    }
  
}
