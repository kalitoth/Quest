using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UI : MonoBehaviour
{
    [SerializeField]
    Slider _sliderMonster;
    [SerializeField]
    Monster _monster;
    [SerializeField]
    Slider _sliderPlayer;
    [SerializeField]
    Player _player;
    [SerializeField]
    GameObject _dieImage;
    [SerializeField]
    Button _restartButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
    }

    void Restart()
    {
        _player.Alive = true;
        Time.timeScale = 1.0f;
        _dieImage.SetActive(false);

        _player.HP = _player.HPMax;
        _player.transform.position = _player.PlayerFirstPosition;
    }
    void Update()
    {
        MonsterSlider();

        PlayerSlider();


        PlayerDied();
    }

    void PlayerDied()
    {
        if (!_player.Alive)
        {
            Time.timeScale = 0.0f;
            _dieImage.SetActive(true);
        }
    }

    void MonsterSlider()
    {
        if(_monster == null)
        {
            return;
        }
        
        _sliderMonster.value = (float)_monster.HP / _monster.HPMax;
         
    }
    void PlayerSlider()
    {
        if(_player == null)
        {
            return;
        }
        _sliderPlayer.value = (float)_player.HP / _player.HPMax;
    }
}
