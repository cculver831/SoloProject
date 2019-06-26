using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;

    public Transform groundCheck;
    private bool isGrounded;
    public float checkradius;
    public LayerMask whatisGround;

    public int extraJumps;
    // Start is called before the first frame update
    void Start()
    {
        extraJumps = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkradius, whatisGround);
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
       
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }
    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
            Debug.Log(extraJumps);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpforce;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
