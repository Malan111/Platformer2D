using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 1f;
    [SerializeField] private Animator animator;



    private float horizontal;

    private bool isGround = false;
    private bool isJump = false;
    private bool isFacingRight = true;
    private bool isFinish = false;
    private bool isLeverArm = false;

    private Rigidbody2D rb;
    private Finish finish;
    private LevelArm levelArm;

    const float speedXMultiplier = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        levelArm = FindObjectOfType<LevelArm>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("speedX", Mathf.Abs(horizontal));

        if (Input.GetKey(KeyCode.W) && isGround)
        {
            isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFinish)
            {
                finish.FinishLevel();
            }
            if (isLeverArm)
            {
                levelArm.ActivateLeverArm();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speedX * speedXMultiplier * Time.fixedDeltaTime, rb.velocity.y);

        if (isJump)
        {
            rb.AddForce(new Vector2(0f, 500f));
            isGround = false;
            isJump = false;
        }

        if (horizontal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0f && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelArm leverArmTemp = collision.GetComponent<LevelArm>();

        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Work");
            isFinish = true;
        }

        if (leverArmTemp != null) 
        {
            isLeverArm = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LevelArm leverArmTemp = collision.GetComponent<LevelArm>();

        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Not Woked");
            isFinish = false;
        }

        if (leverArmTemp != null)
        {
            isLeverArm = false;
        }
    }


}
 