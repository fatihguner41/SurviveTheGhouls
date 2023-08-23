using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform body;
    public float sensitivity=100f;
    public float  xRot=0f;

    //public int target = 60;

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
       float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        
        xRot -= mouseY;
        xRot=Mathf.Clamp(xRot, -80, 80);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        body.Rotate(mouseX*Vector3.up);

       
       
    }
}
