using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMPro : MonoBehaviour
{
    private FSM fsm;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Rigidbody2D rigidBody;
    
    [Header("检测逻辑字段")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Vector3 facingDirection;
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    
    [Header("巡逻逻辑字段")]
    [SerializeField] private float patrolTurnTime;
    [SerializeField] private float patrolRealTurnTime;
    [SerializeField] private float patrolSpeed;

    [Header("追击逻辑字段")]
    [SerializeField] private float chasingKeepTime;
    [SerializeField] private float chasingKeepRealTime;
    [SerializeField] private float chasingSpeed;

    [Header("攻击逻辑字段")] 
    [SerializeField] private float attackIntervalTime;
    [SerializeField] private float attackIntervalRealTime;

    [Header("画线函数字段")]
    [SerializeField] private bool drawLine;
    [SerializeField] private int drawLineType;

    
    void Start()
    {
        fsm = new FSM();
        fsm.AddState(StateType.Patrol,new EnemyPatrol(gameObject,fsm));
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnUpdateState();
        
    }
    
    //这个方法一般是给子状态用的，这里为了方便直接在挂载脚本中用了
    public StateType UpdateState()
    {
        StateType newState = StateType.Patrol;
        
        RaycastHit2D chasingHit = Physics2D.Raycast(transform.position, facingDirection, detectionRange, whatIsPlayer);
        RaycastHit2D attackHit = Physics2D.Raycast(transform.position, facingDirection, attackRange, whatIsPlayer);
        //追击状态保持计时
        if (chasingHit.collider != null || attackHit.collider != null)
        {
            chasingKeepRealTime =  chasingKeepTime;
        }
        else
        {
            chasingKeepRealTime -= Time.deltaTime;
        }
        //状态切换
        if (attackHit.collider != null && attackHit.collider.CompareTag("Player"))
        {
            if(targetTransform == null)
            {
                targetTransform = attackHit.collider.transform;
            }
            Debug.Log("Attacking");
            newState = StateType.Attacking;
        }
        else if (chasingHit.collider != null && chasingHit.collider.CompareTag("Player"))
        {
            if (targetTransform == null)
            {
                targetTransform = chasingHit.collider.transform;
            }
            newState = StateType.Chasing;
        }
        else if (chasingKeepRealTime >= 0)
        {
            newState = StateType.Chasing;
        }
        
        

        return newState;
    }
}
