using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detection : MonoBehaviour
{
    public EnemyMoveMent EnemyMoveMent;
    private bool _iscoroutines = false;

    void Start()
    {
        Init();
    }
    public void Init()
    {
        if (_iscoroutines) return;
        EnemyMoveMent = transform.parent.GetComponent<EnemyMoveMent>();
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
                EnemyMoveMent.TartgetPos = collision.transform.position;
                EnemyMoveMent.EnemyState = EnemyState.Move;
                EnemyMoveMent.IsOnTarget = true;
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                EnemyMoveMent.TartgetPos = collision.transform.position;
                EnemyMoveMent.IsOnTarget = true;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                EnemyMoveMent.EnemyState = EnemyState.Idle;
                EnemyMoveMent.TartgetPos = Vector3.zero;
                EnemyMoveMent.IsOnTarget = false;
                break;
        }
    }
}
