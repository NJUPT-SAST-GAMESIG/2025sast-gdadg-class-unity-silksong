using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CharactorMove : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float upForce = 2.0f;
    
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private float groundCheckStartOffset = 0.1f;

    public bool isGrounded = false;

    void Start()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

    }

    void Update()
    {
        CheckGrounded();
        Move();
        Jump();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
            transform.localScale = new Vector3(1, 1, 0);
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            transform.localScale = new Vector3(-1, 1, 0);

        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&isGrounded)
        {
            rigidbody2D.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
        }
    }
    
    private void CheckGrounded()
    {
        isGrounded = false;
        Vector3 startPostion = transform.position - new Vector3(0,groundCheckStartOffset,0);
        
        
        RaycastHit2D hit = Physics2D.Raycast(
            startPostion,
            Vector2.down, 
            groundCheckDistance, 
            LayerMask.GetMask("Ground")
        );

        if (hit.collider != null)
        {
            isGrounded = true;
        }
            // isGrounded = hit.collider.CompareTag("Ground") && rigidbody2D.velocity.y <= 0.1f;
        
        
    }


    private void OnDrawGizmosSelected()
    {
        Vector3 startPostion = transform.position - new Vector3(0,groundCheckStartOffset,0);
        Gizmos.color =  Color.green ;
        Gizmos.DrawWireSphere(transform.position ,0.05f);
        Gizmos.DrawLine(startPostion, startPostion + Vector3.down * groundCheckDistance);
    }
}
