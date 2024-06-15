using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : MonoBehaviour
{
    [SerializeField] private EnemyMoveMent _enemyMoveMent;
    private bool _iscoroutines = false;

    void Start()
    {
        Init();
    }
    public void Init()
    {
        if (_iscoroutines) return;
        _enemyMoveMent = transform.parent.GetComponent<EnemyMoveMent>();
        _iscoroutines = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                _enemyMoveMent.EnemyState = EnemyState.Attack;
                _enemyMoveMent.IsAttackReady = true;
                
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                _enemyMoveMent.EnemyState = EnemyState.Move;
                _enemyMoveMent.TartgetPos = collision.transform.position;
                _enemyMoveMent.IsAttackReady = false;
                break;
        }
    }
}
