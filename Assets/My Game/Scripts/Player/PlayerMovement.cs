using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("TrannChanhh/PlayerMovement")]

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Move")]
    public float speedMove = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHigh = 3f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;

    private CharacterController controller;
    private Vector3 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCheckGround() && velocity.y < 0)  
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z);
        controller.Move(move * speedMove * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && IsCheckGround())
        {
            velocity.y = Mathf.Sqrt(jumpHigh * gravity * -2f);
        }
    }
    bool IsCheckGround()
    {
        bool isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        return isGround;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
