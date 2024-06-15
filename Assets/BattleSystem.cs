using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem I;
    public float PlayerHP { get; private set; } = 100f;
    public float PlayerAttackDamege { get; private set; } = 2f;
    public HashSet<EnemyMoveMent> TargetList = new HashSet<EnemyMoveMent>();

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerBeAttacked(float Damege)
    {
        PlayerHP -= Damege;
        if(PlayerHP <= 0)
        {
            GameManager.I.EndGame();
        }
    }

    public void PlayerAttackEnemy()
    {
        foreach(EnemyMoveMent ment in TargetList)
        {
            ment.EnemyBeAttacked(PlayerAttackDamege);
        }
    }

}
