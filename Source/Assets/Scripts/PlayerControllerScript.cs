using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    const int WALK_MODIFIER = 4;
    const int RUN_MODIFIER = 7;
    const int JUMP_MODIFIER = 300;


    Rigidbody rigid;

    public int cameraSensitivity;
    public Camera playerCamera;

    //float rotationX = 0F;
    float rotationY = 0F;
    //Quaternion originalRotation;

    float distToGround;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        ProcessMovement();
        ProcessRotation();
    }

    private void ProcessMovement()
    {
        int speedModifier = Input.GetKey("left shift") ? RUN_MODIFIER : WALK_MODIFIER;

        rigid.MovePosition(transform.position + (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right).normalized * speedModifier * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigid.AddForce(transform.up * JUMP_MODIFIER, ForceMode.Impulse);
        }
    }

    private void ProcessRotation()
    {
        rigid.rotation = Quaternion.Euler(rigid.rotation.eulerAngles + new Vector3(0f, cameraSensitivity * Input.GetAxis("Mouse X"), 0f));

        rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity;
        rotationY = Mathf.Clamp(rotationY, -60, 60);

        playerCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
