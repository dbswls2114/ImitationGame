using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _controller;
    private Rigidbody2D _rb;

    private Vector2 _playerMovement;
    private float _moveSpeed = 5f;

    [SerializeField] private float _jumpForce = 14f;
    private int _jumpingcountMax = 2;
    private int _jumpingcount;

    private float _dashDelay = 1f;
    private float _dashForce = 1f;
    private int _dashCountMax = 2;
    private int _dashCount;

    [SerializeField] private float _lookat = 0;
    private bool _iscoroutines = false;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _controller.OnMoveEvent += Move;
        _controller.OnJumpEvent += Jump;
        _controller.OnDashEvent += Dash;
        _jumpingcount = _jumpingcountMax;
        _dashCount = _dashCountMax;
    }

    void FixedUpdate()
    {        
        transform.Translate(_playerMovement.normalized * Time.deltaTime * _moveSpeed);
        if (_lookat < 0f)
        {
            transform.GetChild(0).eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            transform.GetChild(0).eulerAngles = Vector3.zero;
        }
    }

    public void Move(Vector2 value)
    {
        if (value != null)
        {
            _playerMovement = new Vector2(value.x, value.y);
            if (_playerMovement.x != 0)
            {
                _lookat = _playerMovement.x;
            }
        }
    }

    public void Jump()
    {
        if (_jumpingcount <= 0)
        {
            return;
        }
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _jumpingcount--;
    }

    public void Dash()
    {
        if (_dashCount <= 0)
        {
            return;
        }


        _rb.velocity = (new Vector2(_lookat, 1f) * (_dashForce + _moveSpeed));
        _dashCount--;
        if (!_iscoroutines)
        {
            StartCoroutine(DashDelay());
        }       
    }

    IEnumerator DashDelay()
    {
        _iscoroutines = true;
        yield return new WaitForSeconds(_dashDelay);
        _dashCount = _dashCountMax;
        _iscoroutines = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ground":
                if (_rb.velocity.y == 0)
                    _jumpingcount = _jumpingcountMax;
                break;
        }
    }
}