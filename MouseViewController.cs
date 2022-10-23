using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseViewController : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;
    public Transform cam;
    public Transform weapon;

    
    float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 30f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        weapon.transform.rotation = cam.transform.rotation;
        Transform p_spine = playerBody.transform.Find("PT_Medieval_Hips/PT_Medieval_Spine/PT_Medieval_Spine2/PT_Medieval_Spine3");
        
        p_spine.transform.rotation = cam.transform.rotation;
    }
}
