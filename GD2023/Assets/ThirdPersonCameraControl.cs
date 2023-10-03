using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class thirdpersoncameracontrol : MonoBehaviour
{
    public float desiredAngle = 0;
    private float bufferZone = 2;

    private float sensitivity = 50f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        desiredAngle += sensitivity * Input.GetAxis("Horizontal") * Time.deltaTime;
        desiredAngle = Mathf.Clamp(desiredAngle, -179f, 179f);
        if (angleIsTooSmall())
        {
            print("Adding 1");
            transform.RotateAround(transform.parent.position, Vector3.up, 1);
        }
        else
            if (angleIsTooBig())
            {
                print("Subtracting");
                print("Euler y is " + transform.localRotation.eulerAngles.y.ToString());
                print("Desired Angle " + desiredAngle.ToString() + " + buffer of " + bufferZone.ToString());
                transform.RotateAround(transform.parent.position, Vector3.up, -1);
            }


        desiredAngle = Mathf.Lerp(desiredAngle, 0, 0.001f);

    }

    private bool angleIsTooBig()
    {
        float angle = adjustAngle180(transform.localRotation.eulerAngles.y);
        return angle > desiredAngle - bufferZone;
    }



    private bool angleIsTooSmall()
    {
        float angle = adjustAngle180(transform.localRotation.eulerAngles.y);
        return angle < desiredAngle + bufferZone;

        
    }

    float adjustAngle180(float angle)
    {
        if (Mathf.Abs(angle) <= 180f) return angle;
        
        if (angle > 180f) return adjustAngle180(angle - 360f);
        return adjustAngle180(angle + 360f);

    }



}