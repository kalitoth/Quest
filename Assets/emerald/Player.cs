using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Unit
{
    Animator _animator;
    Rigidbody _rb;

    [SerializeField]
    float _speed = 3f;
    [SerializeField]
    float _angleSpeed = 3f;
     
    RaycastHit _hit;
    LayerMask _layer;
    string _groundTag = "Ground";
    float _rayDistance = 0.3f;
    bool _isGround;

    [SerializeField]
    float _jumpForce = 4f;

    string _moveParam = "IMove";

    [Header("╬Нец")]
    string _attackTrigger = "TAttack";
    float _attackInterval = 1f;
    float _time;
   [SerializeField]
   GameObject _weapon;
    GameObject _attack;

    Vector3 _playerFirstPosition;
    public Vector3 PlayerFirstPosition => _playerFirstPosition;
    State _state = State.None;

     enum State
    { 
        None,
        Walk, 
    }
      
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _layer = 1 << LayerMask.NameToLayer(_groundTag);

        _playerFirstPosition = transform.position;
       HP = HPMax;
    }

    void Update()
    {
       
        Walk();
        IsGround();
        MoveAnimator();
        Attack();
        Die();
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();
        jump();
    }

    
    void Attack()
    {
        
        _time += Time.deltaTime;

        
        if (_time <= _attackInterval)
        {
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger(_attackTrigger);
             
            
            _attack = Instantiate(_weapon,transform.position+ transform.forward + transform.up*0.8f  , transform.rotation);

            Destroy(_attack.gameObject,0.1f);  

            _time = 0;

        }

       
    }
   
    
    void Walk()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed *= 0.5f;

            _state = State.Walk;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed *= 2f;

            _state = State.None;
        }
    }
    void MoveAnimator()
    {
         
        if(_state == State.Walk)
        {
            if (Getkey())
            {
                _animator.SetInteger(_moveParam, 1);
            }
        }
        else if(_state == State.None)
        {
            if (Getkey())
            {
                _animator.SetInteger(_moveParam, 2);
            }
        }
         
        if (!Getkey())
        {
            _animator.SetInteger(_moveParam, 0);
        }

    }

   bool Getkey()
    {
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            return true;
        }
        return false;
    }

    private void Move()
    {
        Vector3 moving = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moving += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moving -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moving -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moving += transform.right;
        }

        moving = moving.normalized * _speed;

        moving.y = _rb.velocity.y;

        _rb.velocity = moving;
    }

    void Rotate()
    {
        float inputMouse = Input.GetAxis("Mouse X");

        Quaternion rotateTarget = transform.rotation * Quaternion.AngleAxis(inputMouse * _angleSpeed, Vector3.up);
        _rb.MoveRotation(rotateTarget);
    }

    void IsGround()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out _hit, _rayDistance, _layer);

            _isGround = _hit.collider == null ? false : true; 
        }
    }

    void jump()
    {

        if (_isGround)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            _animator.SetTrigger("TJump");
            _isGround = false;
        }

    }
}
