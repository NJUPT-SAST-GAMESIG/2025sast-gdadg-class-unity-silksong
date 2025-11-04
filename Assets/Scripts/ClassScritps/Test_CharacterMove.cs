using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CharactorMove : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float upForce = 2.0f;
    [SerializeField] private bool isGrounded;

    void Start()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        Debug.Log("rigidbody2D.velocity");
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.velocity = new Vector2(speed , rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody2D.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
