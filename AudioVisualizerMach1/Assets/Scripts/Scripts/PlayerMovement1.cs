using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    [Range(5, 15)]
    public float speed;
    [Range(5, 15)]
    public float jumpforce;
    private float moveInput;
    private Rigidbody rb;

    /// <summary>
    /// Jumping Script Starts here
    /// </summary>
   [Range(1, 5)]
    public float fallMultiplier = 2.5f;
    [Range(1, 5)]
    public float lowJumpMultiplier = 2f;
    
    public Transform groundCheck;
    private bool isGrounded;
   
    public int extraJumps;
    // Start is called before the first frame update
    void Awake()
    {
        extraJumps = 1;
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(moveInput * speed, rb.velocity.y);
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.tag == "Floor")
        {
            isGrounded = false;
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
}
