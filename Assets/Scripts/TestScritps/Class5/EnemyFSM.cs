using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyFSM : MonoBehaviour
{
    //组件引用
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


    private enum EnemyState
    {
        Patrol,
        Chasing,
        Attacking,
        Die
    }

    [SerializeField] private EnemyState enemyState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //状态机
        switch (enemyState)
        {
            case EnemyState.Patrol:
                OnPatrol();
                break;
            case EnemyState.Chasing:
                OnChasing();
                break;
            case EnemyState.Attacking:
                OnAttacking();
                break;
            case EnemyState.Die:
                OnDie();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private EnemyState UpdateState()
    {
        EnemyState newState = EnemyState.Patrol;
        
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
            newState = EnemyState.Attacking;
        }
        else if (chasingHit.collider != null && chasingHit.collider.CompareTag("Player"))
        {
            if (targetTransform == null)
            {
                targetTransform = chasingHit.collider.transform;
            }
            newState = EnemyState.Chasing;
        }
        else if (chasingKeepRealTime >= 0)
        {
            newState = EnemyState.Chasing;
        }
        
        

        return newState;
    }

    private void OnPatrol()
    {
        //巡逻逻辑
        //转身计时重置
        if (patrolRealTurnTime <= 0)
        {
            patrolRealTurnTime = patrolTurnTime;
            facingDirection.x *= -1;
            transform.localScale = new Vector3(facingDirection.x * -1, 1, 1);
        }
        //行进
        rigidBody.velocity = new Vector3(facingDirection.x * patrolSpeed, rigidBody.velocity.y);
        patrolRealTurnTime -= Time.deltaTime;
        
        //更新状态
        enemyState = UpdateState();

    }

    private void OnChasing()
    {
        //追击逻辑
        facingDirection = new Vector3(targetTransform.position.x > transform.position.x ? 1 : -1, 0, 0);
        transform.localScale = new Vector3(facingDirection.x * -1, 1, 1);
        rigidBody.velocity =new Vector3(chasingSpeed * facingDirection.x, rigidBody.velocity.y ,0);
        Debug.Log("Chasing");
        
        //更新状态
        enemyState =  UpdateState();
    }

    private void OnAttacking()
    {
        //攻击逻辑
        if (attackIntervalRealTime <= 0)
        {
            rigidBody.velocity = Vector3.zero;
            attackIntervalRealTime = attackIntervalTime;
            //ExecuteAttack() 调用造成伤害的函数
            Debug.Log("Manbo");
        }
        else
        {
            attackIntervalRealTime -= Time.deltaTime;
        }

        enemyState = UpdateState();
    }

    private void OnDie()
    {
        //死亡逻辑
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (drawLine)
        {
            if (drawLineType == 1)
            {
                Gizmos.DrawLine(transform.position, transform.position + facingDirection * detectionRange);
            }
            else if (drawLineType == 2)
            {
                Gizmos.DrawLine(transform.position, transform.position + facingDirection * attackRange);
            }
        }
        
    }
}
    
