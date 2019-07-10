using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerTransform;
    public float forwardSpeed = 200f;
    public float sidewaysForce = 2f;
    public float jumpForce = 15f;
    public float extendedGravity = 1000f;
    private float sidewaysMove;
    private float maxWidth = 40f;
    private float maxHeight = 2f;
    private Vector3 playerPos;
    private bool isMidJump;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector3(0f, 0f, 0f);
        isMidJump = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        sidewaysMove = Input.GetAxisRaw("Horizontal");
        //moving the character
        //rb.AddForce(0, 0, forwardSpeed * Time.deltaTime);
        rb.AddForce(0, -extendedGravity * Mathf.Pow(playerTransform.position.y, 0.5f) * Time.deltaTime, 0);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
        playerPos.x += sidewaysForce * sidewaysMove * Time.deltaTime;
        playerPos.y = playerTransform.position.y;
        playerPos.z = playerTransform.position.z;
        if (playerPos.x > maxWidth / 2)
            playerPos.x = maxWidth / 2;
        if (playerPos.x < -maxWidth / 2)
            playerPos.x = -maxWidth / 2;
        if (playerPos.y > maxHeight)
            playerPos.y = maxHeight;//rb.AddForce(0, -2000*Time.deltaTime, 0);
        if (playerPos.y < 0)
            playerPos.y = 0;
        playerTransform.position = playerPos;
        //jump mechanics
        if (!isMidJump && Input.GetAxisRaw("Vertical") > 0)
            Jump();
        if (isGrounded)
        {
            isMidJump = false;
            maxHeight = 2f;
        }
        //rb.AddForce(sidewaysForce * sidewaysMove * Time.deltaTime, 0, 0);
        //playerTransform.Translate(sidewaysForce * sidewaysMove * Time.deltaTime, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    void Jump()
    {
        maxHeight = 20f;
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        isMidJump = true;
        isGrounded = false;
    }
}
