using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject playerCamera;
    public float mouseSensitivityX = 300f;
    public float mouseSensitivityY = 300f;
    public float movementSpeed = 4f;
    public float sprintMultiplier = 2f;
    public LayerMask groundMask;
    Rigidbody rigidbody;
    float xRotation;
    public bool grounded;
    public float jumpImpulse = 10f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float Jump = Input.GetAxis("Jump");
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(transform.up, mouseX * mouseSensitivityX * Time.deltaTime);
        xRotation -= mouseY * mouseSensitivityY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0 ,0);

        float sprintSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1;
        float finalSpeed = movementSpeed * sprintSpeed;

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        rigidbody.MovePosition(moveDirection.normalized * finalSpeed * Time.deltaTime + rigidbody.position);

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rigidbody.AddForce(0, jumpImpulse, 0, ForceMode.Impulse);
            }
        }
        grounded = false;
        Ray groundRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(groundRay, out hit, 1.1f, groundMask))
        {
            grounded = true;
        }
    }
}
