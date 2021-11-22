


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;
    private Animator animator;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    private float jumpForce = 850;
    private int groundMask;
    private bool isGrounded;
    private string currentAnimation;
    private bool isAttackPressed;
    private bool isAttacking;

    [SerializeField]
    private float attackDelay = 0.3f;

    //Animation states
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_RUN = "Player_run";
    const string PLAYER_JUMP = "Player_jump";
    const string PLAYER_ATTACK = "Player_attack";
    const string PLAYER_AIR_ATTACK = "Player_air_attack";


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
        
    }


    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;

        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            isAttackPressed = true;
        }


    }



    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }

        Vector2 vel = new Vector2(0, rb2d.velocity.y);




        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);

        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);


        }
        else
        {
            vel.x = 0;

        }

        if (xAxis != 0)
        {
            ChangeAnimationState(PLAYER_RUN);

        }
        else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }




        if (isJumpPressed && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
        }
        rb2d.velocity = vel;


        if (isAttackingPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;
            }
        }

    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation)
            return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

    void GroundCheck()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.green);

        if (hit.collider != null)
        {
            isGrounded = true;


        }
        else
        {
            isGrounded = false;
        }


    }


 

}







