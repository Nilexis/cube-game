using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerTransform;
    public float forwardSpeed = 200f;
    public float sidewaysForce = 2f;
    private float sidewaysMove;
    private float maxWidth = 40;
    private float maxHeight = 2;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector3(0f, 0f, 0f);
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
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
        playerPos.x += sidewaysForce * sidewaysMove * Time.deltaTime;
        playerPos.y = playerTransform.position.y;
        playerPos.z = playerTransform.position.z;
        if (playerPos.x > maxWidth / 2)
            playerPos.x = maxWidth / 2;
        if (playerPos.x < -maxWidth / 2)
            playerPos.x = -maxWidth / 2;
        if (playerPos.y > maxHeight)
            playerPos.y = maxHeight;
        playerTransform.position = playerPos;
        //rb.AddForce(sidewaysForce * sidewaysMove * Time.deltaTime, 0, 0);
        //playerTransform.Translate(sidewaysForce * sidewaysMove * Time.deltaTime, 0f, 0f);

    }
}
