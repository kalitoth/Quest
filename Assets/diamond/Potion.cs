using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events; 
using UnityEngine.UI;

public class Potion<T> : MonoBehaviour
{
    [SerializeField]
    Player _player;
     
    [SerializeField]
    GameObject _item;
    [SerializeField]
    Button _itemButton;

    [SerializeField]
    Button _activeButton;

    Button _potionButton;
    TextMeshProUGUI _potionText;

    public int _potionNum;
    bool _selectButton;
     
    public Action _potionAction;

    public static Potion<T> instance;

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
        if (instance == this)
        {
            instance = null;
        }
    }
    void Start()
    {
        _potionAction += GetPotion;
    }
     
    void GetPotion()
    {
        _potionNum++;

        if (_potionNum != 0 && _potionButton == null)
        {
            _potionButton = Instantiate(_itemButton, _item.transform);
            Image _buttonImage = _potionButton.GetComponent<Image>();
            TextMeshProUGUI[] text = _potionButton.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].text = "Potion";
            _potionText = text[1];

            _potionButton.onClick.AddListener(() =>
            {
                _selectButton = !_selectButton;

                SelectButton(() => { UsePotion(_buttonImage); });
                ToggleButtonColor(_buttonImage);
            });

        }

        if (_potionButton == null)
        {
            return;
        }

        _potionText.text = $"{_potionNum}";

        if (!_potionButton.IsActive())
        {
            if (_potionNum == 1)
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

        int heal = 1;
        _potionNum--;
        _player.TakeHeal(heal);

        _potionText.text = $"{_potionNum}";

        if (_potionNum == 0)
        {
            _potionButton.gameObject.SetActive(false);
            _activeButton.onClick.RemoveAllListeners();
            color.color = Color.white;
            _selectButton = false;

        }

    }
}
