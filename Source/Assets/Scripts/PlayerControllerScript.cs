using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    Rigidbody rigid;

    public int speedModifier;
    public int cameraSensitivity;
    public Camera playerCamera;

    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
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
        rigid.MovePosition(transform.position + (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right).normalized * speedModifier*Time.deltaTime);
    }

    private void ProcessRotation()
    {
        rigid.rotation = Quaternion.Euler(rigid.rotation.eulerAngles + new Vector3(0f, cameraSensitivity * Input.GetAxis("Mouse X"), 0f));

        rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity;
        rotationY = Mathf.Clamp(rotationY, -60, 60);

        playerCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);

    }
}
