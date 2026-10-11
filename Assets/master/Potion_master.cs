using System;
using System.Collections;
using System.Collections.Generic; 
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Potion_master<T> : MonoBehaviour, IPotion_master
{
    [SerializeField]
    Player_master _player;

    [SerializeField]
    GameObject _item;
    [SerializeField]
    Button _itemButton;

    [SerializeField]
    Button _activeButton;

    Button _potionButton;
    TextMeshProUGUI _potionNumText;
     

    public int _potionNum;
    bool _selectButton;

    protected int _potionAmount;
    protected string _potionName; 

    public Action<int> _potionAction;

    public static Potion_master<T> instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;

        _potionAction += GetPotion; 
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
     
    void GetPotion(int num = 1)
    {
        _potionNum += num;
         

        if (_potionNum != 0 && _potionButton == null)
        {
            _potionButton = Instantiate(_itemButton, _item.transform);
            Image _potionImage = _potionButton.GetComponent<Image>();
            TextMeshProUGUI[] text = _potionButton.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].text = _potionName;
            _potionNumText = text[1];

            IPotion_master.IpotionImage = _potionImage;

            _potionButton.onClick.AddListener(() =>
            { 
                _selectButton = !_selectButton;

                if(IPotion_master.IpotionImage == _potionImage)
                {
                    IPotion_master.IpotionImage = _potionImage;
                }
                else
                {
                    _selectButton = true;
                    IPotion_master.IpotionImage.color = Color.white; 
                    IPotion_master.IpotionImage = _potionImage;
                }


                    SelectButton(() => { UsePotion(IPotion_master.IpotionImage); });
                ToggleButtonColor(IPotion_master.IpotionImage);
            });

        }

        if (_potionButton == null)
        {
            return;
        }

        _potionNumText.text = $"{_potionNum}";

        if (!_potionButton.IsActive())
        {
            if (_potionNum >= 1)
            {
                _potionButton.gameObject.SetActive(true);

            }
        }

    }
    void SelectButton(UnityAction action)
    {
        _activeButton.onClick.RemoveAllListeners();

        if (_selectButton)
        {
            Debug.Log("1");
            _activeButton.onClick.AddListener(action);
        }
        else
        {
            Debug.Log("2");
            _activeButton.onClick.RemoveAllListeners();
        }
    }
     
    void ToggleButtonColor(Image color)
    { 
        color.color = _selectButton ? Color.red : Color.white;
    }

    
    void UsePotion(Image color)
    {
        if (_potionNum <= 0)
        {
            return;
        }


        _potionNum--;
        _player.TakeHeal(_potionAmount);

        _potionNumText.text = $"{_potionNum}";

        if (_potionNum <= 0)
        {
            _potionButton.gameObject.SetActive(false);
            _activeButton.onClick.RemoveAllListeners();
            color.color = Color.white;
            _selectButton = false;

        }

    }
}
