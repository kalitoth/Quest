using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PotionStore_master : MonoBehaviour
{
    [SerializeField]
    Button[] _selectButton ; // 버튼 
    [SerializeField]
    TextMeshProUGUI[] _selectText ; //수량 , 가격

    [SerializeField]
    Button _buyButton; // 구매 버튼
    [SerializeField]
    TextMeshProUGUI _text; // 총 수량 , 가격 

    [SerializeField]
    Button _cancelButton; // 취소 버튼

    [SerializeField]
    Image _failImage;

    int[] _num = new int[2] {5,2};
    int[] _numSum = new int[2];
    int[] _price = new int[2] { 800, 300 };

    int _priceSum; 

    string _playerTag = "Player";

    float _time;
    float _Interval = 1f;
    private void Awake()
    {
        _buyButton.onClick.AddListener(BuyButton);
         
        

        _cancelButton.onClick.AddListener(CancelSum);
         
    }
    private void Start()
    {
        for (int i = 0; i < _num.Length; i++)
        {
            int j = i;
            _selectButton[j].onClick.AddListener(() => { SelectButton(ref _num[j], ref _price[j], ref _numSum[j]); });
        }

        if (Gold_master.instance == null)
        {
            Debug.Log("Gold_master가 널");
        }

        ItemRefresh();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == _playerTag)
        {
            CancelSum();
        }
    }
    //셀렉 버튼
    void SelectButton(ref int num, ref int price, ref int numSum)
    {
        if(Gold_master.instance == null)
        {
            Debug.Log("널");
            return;
        }
        if(num <= 0)
        {
            return;
        }
        
        _priceSum += price;

        if (Gold_master.instance._goldNum < _priceSum)
        {
            Debug.Log("돈이 충분하지 않다");
            if(_time <= 0)
            {
                StartCoroutine(Co_Fail());
            }
            _priceSum -= price;
            return;
        }

        num--;
        numSum++;
        // _numSum++; 
        ItemRefresh();
    }
    //buy 버튼
    void BuyButton()
    { 
        Gold_master.instance._goldNum -= _priceSum;
        HealingPotion_master.instance._potionAction(_numSum[0]);
        DamagePotion_master.instance._potionAction(_numSum[1]);
        _priceSum = 0;

        for(int i = 0; i < _numSum.Length; i++)
        {
            _numSum[i] = 0;
        }
        //_numSum = 0;

        ItemRefresh();
    }

    void CancelSum()
    {
        _priceSum = 0; 
        for(int i = 0; i <_num.Length; i++)
        {
            _num[i] += _numSum[i];
            _numSum[i] = 0;
        } 

        ItemRefresh();
    }

     
    void ItemRefresh()
    {
        for(int i = 0; i < _num.Length;i++)
        {
            _selectText[i].text = $"Remain : {_num[i]}\nPrice : {_price[i]}";
        } 
        _text.text = $"Total Price : {_priceSum}";
        Gold_master.instance._goldAction.Invoke();

    }

    IEnumerator Co_Fail()
    {
        _failImage.gameObject.SetActive(true);

        _time = _Interval;
        Color alpha = _failImage.color;

        while (_time >= 0)
        {
            
            alpha = new Color(alpha.r, alpha.g, alpha.b, (_time / _Interval));
            _failImage.color = alpha;
           _time -= Time.deltaTime;
            yield return null;
        }

        alpha =  new Color(alpha.r, alpha.g, alpha.b, 0);
        _failImage.color = alpha;

        _failImage.gameObject.SetActive(false);
    }
}
