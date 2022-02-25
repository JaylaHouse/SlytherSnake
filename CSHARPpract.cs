using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    [SerializeField] private Transform groundCheckTransform = null;
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if space key is pressed down
        if(Input.GetKeyDown(KeyCode.Space) == true){
            Debug.Log("Space Key Was Pressed Down");
            jumpKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");

    }

    // FixedUpdate is called once every physics update
    private void FixedUpdate(){

        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1){
            return;
        }

        if(isGrounded == false){

            return;

        }

        if(jumpKeyPressed == true){
        rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
        jumpKeyPressed = false;
        }

        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y , 0);
    }

    private void OnCollisionEnter(Collision collision){

        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision){
        isGrounded = false;
    }

}
