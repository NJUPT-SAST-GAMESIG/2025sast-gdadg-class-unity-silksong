using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyFSM : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Transform target;
    
    [SerializeField] private float facingDirection;
    
    
    enum EnemyState
    {
        Patrol,
        Chasing,
        Attacking,
        Dead
    }
    
    //检测字段
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float chasingRange;
    [SerializeField] private float attackRange;
    
    //巡逻字段
    [SerializeField] private float patrolTurnTime;
    [SerializeField] private float patrolTurnRealTime;
    [SerializeField] private float patrolSpeed;
    
    //追击字段
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float chaseTurnTime;
    [SerializeField] private float chaseTurnRealTime;
    
    //攻击字段
    [SerializeField] private float attackTime;
    [SerializeField] private float attackRealTime;

    [SerializeField] private EnemyState enemyState;
    
    // Start is called before the first frame update
    void Start()
    {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            case EnemyState.Dead:
                //死亡
                break;
            default:
                break;
        }
    }

    private void OnPatrol()
    {
        if (patrolTurnRealTime < 0)
        {
            patrolTurnRealTime = patrolTurnTime;
            facingDirection *= -1;
            transform.localScale = new Vector3(facingDirection, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            patrolTurnRealTime -= Time.deltaTime;
        }
        
        rigidbody.velocity = new Vector2(facingDirection * patrolSpeed * -1, rigidbody.velocity.y);
        
        enemyState = UpdateState();
    }

    private void OnChasing()
    {
        facingDirection = target.position.x > transform.position.x ? -1 : 1;
        transform.localScale = new Vector3(facingDirection, transform.localScale.y, transform.localScale.z);
        rigidbody.velocity = new Vector2(facingDirection * chaseSpeed * -1, rigidbody.velocity.y);
        
        enemyState = UpdateState();
    }

    private void OnAttacking()
    {
        if (attackRealTime < 0)
        {
            rigidbody.velocity = Vector2.zero;
            attackRealTime = attackTime;
            Debug.Log("ManBo");
        }
        else
        {
            attackRealTime -= Time.deltaTime;
        }

        enemyState = UpdateState();
    }

    private EnemyState UpdateState()
    {
        EnemyState rState = EnemyState.Patrol;
        
        RaycastHit2D chasingHit = Physics2D.Raycast(transform.position, new Vector2(facingDirection,0),chasingRange,whatIsPlayer);
        RaycastHit2D attackHit = Physics2D.Raycast(transform.position, new Vector2(facingDirection,0),attackRange,whatIsPlayer);

        if (attackHit.collider != null && attackHit.collider.CompareTag("Player"))
        {
            rState = EnemyState.Attacking;
        }

        else if (chasingHit.collider != null && chasingHit.collider.CompareTag("Player"))
        {
            rState = EnemyState.Chasing;
        }
        
        return rState;
    }
}
