using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    [Range(1, 100)]
    public float moveSpeed = 1f;
    [Range(1, 20)]
    public float jumpForce = 1f;
    public float glidingSpeed;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float moveInput;
    private float _initalGravitySpeed;

    private bool facingRight = true;

    private bool isGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpingTime;

    private Vector2 _respawnPoint;
    private Collider2D _collider;
    private float bombs = 0;

    [SerializeField]
    private bool _active = true;

    private Rigidbody2D rb;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _initalGravitySpeed = rb.gravityScale;
        _collider = GetComponent<Collider2D>();
        SetRespawnPoint(transform.position);
    }

    void Update()
    {
        if (!_active)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space) && rb.velocity.y <= 0)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, -glidingSpeed);
        }
        else
        {
            rb.gravityScale = _initalGravitySpeed;
        }

        //Jump
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("takeOff");
            isJumping = true;   
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        if (isGround == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else 
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        //Better Jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //Wall Sliding
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isTouchingFront == true && isGround == false && moveInput != 0)
        {
            wallSliding = true;
            anim.SetBool("isWallSliding", true);
        }
        else
        {
            wallSliding = false;
            anim.SetBool("isWallSliding", false);
        }
       
        //Wall Jump
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpingTime);
        }

        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }

        if(bombs >= 39)
        {
            gameManager.FinishGame();
        }
    }
    //Collect
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            ScoreSystem.scoreValue += 750;
            bombs += 1f;
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("PickUp");
        }
    }

    private void MiniJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);
    }

    public void Die()
    {
        _active = false;
        _collider.enabled = false;
        MiniJump();
        StartCoroutine(Respawn());
    }

    private void SetRespawnPoint(Vector2 position)
    {
        _respawnPoint = position;
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    void FixedUpdate()
    {
        //Player Movement 
        moveInput = Input.GetAxis("Horizontal");
        if (wallJumping != true)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else 
        {
            anim.SetBool("isRunning", true);
        }

        if (facingRight == false && moveInput > 0) 
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

    }

    //char Flip
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    //Respawn
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        transform.position = _respawnPoint;
        _active = true;
        _collider.enabled = true;
        MiniJump();
    }


}
