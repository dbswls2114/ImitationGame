using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerController _controller;

    private Animator _animator;
    private Ghost _ghost;

    private bool _initialize = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (_initialize)
            return;

        _animator = GetComponent<Animator>();
        _ghost = GetComponent<Ghost>();
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _controller.OnMoveEvent += MoveAnimation;
        _controller.OnAttackEvent += AttackAnimation;
        _controller.OnDashEvent += DashAnimation;

        _initialize = true;
    }

    public void MoveAnimation(Vector2 value)
    {
        _animator.SetTrigger("doMove");
    }
    public void AttackAnimation()
    {
        _animator.SetTrigger("doAttack");
    }
    public void DashAnimation()
    {
        if (!_ghost.IsmakeGhost)
        {
            StartCoroutine(DashOff());
        }
    }
    IEnumerator DashOff()
    {
        _ghost.IsmakeGhost = true;
        yield return new WaitForSeconds(1f);
        _ghost.IsmakeGhost = false;
    }
}
