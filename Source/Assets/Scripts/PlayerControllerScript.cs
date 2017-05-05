using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

    Rigidbody rigid;

    public int speedModifier;
    public int cameraSensitivity;
    public Camera playerCamera;

    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rigid.velocity = Input.GetAxis("Vertical") * speedModifier * transform.forward;

        rigid.rotation = Quaternion.Euler(rigid.rotation.eulerAngles + new Vector3(0f, cameraSensitivity * Input.GetAxis("Mouse X"), 0f));
        playerCamera.transform.rotation = playerCamera.transform.rotation * Quaternion.Euler(-cameraSensitivity * Input.GetAxis("Mouse Y"), 0f , 0f);

        if(playerCamera.transform.localRotation.eulerAngles.x > 60)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(60, playerCamera.transform.localRotation.y, playerCamera.transform.localRotation.z);
        }
        if (playerCamera.transform.localRotation.eulerAngles.x < -60)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(-60, playerCamera.transform.localRotation.y, playerCamera.transform.localRotation.z);
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
         angle += 360F;
        if (angle > 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
