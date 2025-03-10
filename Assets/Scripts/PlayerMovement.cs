using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float wallJump;
    private float horizontalInput;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Player Movement
        horizontalInput = Input.GetAxis("Horizontal");

        //Player Flip Left-Right
        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (Input.GetAxis("Horizontal") < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("ground", isGround());

        //Player Walljump
        if (wallJump > 0.2f)
        {

            rb2D.velocity = new Vector2(horizontalInput * speed, rb2D.velocity.y);

            if (onWall() && !isGround())
            {
                rb2D.gravityScale = 0;
                rb2D.velocity = Vector2.zero;
            }
            else
            {
                rb2D.gravityScale = 2;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }
        else
        {
            wallJump += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGround())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGround())
        {
            if (horizontalInput == 0)
            {
                rb2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else
            {
                rb2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 1, 3);
            }
            wallJump = 0;
        }
    }
    private bool isGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGround() && !onWall();
    }
}
