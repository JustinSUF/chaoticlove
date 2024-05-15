using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxHealth = 100;

    private int currentHealth;
    private bool isGrounded = false;
    private bool canDoubleJump = true;

    Rigidbody2D rb;
    Animator animator;

    private float boundary = -1f ;
    public GameObject player;
    public GameObject RespawnPlayer;

    public float glideGravity = 0.5f;
    public float normalGravity = 1f;
    public bool isGliding = false;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

        // Flip character based on movement direction
        if (moveInput > 0) // Moving right
        {
            transform.localScale = new Vector3(1, 1, 1); // No flipping needed
        }
        else if (moveInput < 0) // Moving left
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flip horizontally
        }


        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGliding = true;

            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
            
        }
        if (transform.position.y < boundary)
        {
            player.transform.position = RespawnPlayer.transform.position;
        }
       

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isGliding = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            
        }
    }


    void FixedUpdate()
    {
        if (isGliding && rb.velocity.y < 0)
        {
            rb.gravityScale = glideGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

}
