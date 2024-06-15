using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Dead
}

public class EnemyMoveMent : MonoBehaviour
{
    public EnemyState EnemyState; // Correctly initialized enum
    public Vector3 TartgetPos;
    public bool IsOnTarget = false;
    public bool IsAttackReady = false;

    private GameObject _attackRange;
    private GameObject _senserange;
    private Rigidbody2D _rigidbody2d;

    private float _attackDelay = 1f;
    private float _attacktime = 1f;

    public float EnemyHP = 10f;

    private bool _iscoroutines = false;
    private bool _isAttacking = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (_iscoroutines) return;
        EnemyState = EnemyState.Idle;
        _rigidbody2d = GetComponent<Rigidbody2D>();

        _attackRange = transform.GetChild(0).gameObject;
        _senserange = transform.GetChild(1).gameObject;

        _iscoroutines = true;
    }
    void Update()
    {
        _attacktime += Time.deltaTime;
        
        switch (EnemyState)
        {
            case EnemyState.Idle:
                _isAttacking = false;
                break;

            case EnemyState.Move:
                if (IsOnTarget)
                {
                    Vector3 a = (TartgetPos - transform.position).normalized;
                    if (a.x > 0)
                    {
                        transform.eulerAngles = Vector3.zero;
                        transform.Translate(a * 3f * Time.deltaTime);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 180f, 0);
                        transform.Translate(-a * 3f * Time.deltaTime);
                    }
                }
                break;

            case EnemyState.Attack:
                //TODO: 공격(실제 데미지를 오가는부분)
                if (!_isAttacking)
                {
                    _rigidbody2d.velocity = new Vector2(((TartgetPos - transform.position).normalized.x), 0.1f) * 5f;
                    _isAttacking = true;
                }
                break;

            case EnemyState.Dead:
                EnemyDead();
                break;
        }
    }

    public void EnemyAttack()
    {

    }
    public void EnemyBeAttacked(float Damege)
    {
        EnemyHP -= Damege;
        if (EnemyHP <= 0 && EnemyState != EnemyState.Dead)
        {
            EnemyState = EnemyState.Dead;
        }
    }
    public void EnemyDead()
    {
        GameManager.I.Enemys.Remove(this.gameObject);
        Destroy(gameObject);
        GameManager.I.StageEndCheck();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "PlayerAttack":             
                BattleSystem.I.TargetList.Add(this); break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "PlayerAttack":
                BattleSystem.I.TargetList.Remove(this); break;
        }
    }

}