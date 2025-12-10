using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : IState
{
    private GameObject enemy;
    private FSM fsm;
    
    private Rigidbody rigidBody;
    [Header("巡逻逻辑字段")]
    private float facingDirection;
    private float patrolTurnTime;
    private float patrolRealTurnTime;
    private float patrolSpeed;

    public EnemyPatrol(GameObject enemy ,FSM fsm)
    {
        //一般数据的传递都需要再建一个数据类来管理，这里为了方便直接传GO了
        this.enemy = enemy;
        this.fsm = fsm;
        rigidBody = enemy.GetComponent<Rigidbody>();
        patrolTurnTime = 4f;
        patrolSpeed = 2f;
    }
    public void OnEnterState()
    {
        facingDirection = enemy.transform.localScale.x;
        patrolRealTurnTime = 0;
    }

    public void OnExitState()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdateState()
    {
        //巡逻逻辑
        //转身计时重置
        if (patrolRealTurnTime <= 0)
        {
            patrolRealTurnTime = patrolTurnTime;
            facingDirection *= -1;
            enemy.transform.localScale = new Vector3(facingDirection * -1, 1, 1);
        }
        //行进
        rigidBody.velocity = new Vector3(facingDirection * patrolSpeed, rigidBody.velocity.y);
        patrolRealTurnTime -= Time.deltaTime;
        
        //判断状态改变逻辑
        if (true)
        {
            fsm.SwitchState(StateType.Patrol);
        }
    }
}
