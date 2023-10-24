using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : Target
{

    bool up;
    bool rotationOn = false;
    float swingTop = 360f;
    float swingBottom = -360f;
    float currentRotationAmount = 0f;

    Quaternion targetRotation;

    Vector3 rotation;

    float time;

    void Start() 
    {
        targetRotation = target.transform.rotation;
    }

    void Update()
    {
        if (rotationOn)
        {
            if(up)
            {
                if (currentRotationAmount < swingTop)
                {
                    time = Time.deltaTime * 2;
                    target.transform.Rotate(rotation * time);
                    currentRotationAmount += swingTop * time;
                }
                else
                {
                    rotationOn = false;
                    target.transform.rotation = targetRotation;
                }
            }
            else
            {
                // Debug.Log("Current Rotation: " + currentRotationAmount);
                if (-currentRotationAmount > swingBottom)
                {
                    time = Time.deltaTime * 2;
                    target.transform.Rotate(rotation * time);
                    currentRotationAmount -= swingBottom * time;
                }
                else
                {
                    rotationOn = false;
                    target.transform.rotation = targetRotation;
                }
            }
        }

    }

    void Rotate(float degrees)
    {
        if (!rotationOn)
        {
            if (degrees > 0)
            {
                up = true;
                swingTop = degrees;
            }
            else 
            {
                up = false;
                swingBottom = degrees;
            }
            rotationOn = true;
            currentRotationAmount = 0f;
            rotation = new Vector3(degrees, 0, 0);
        }
    }

    public override void Process(RaycastHit hit)
    {
        if (gameObject.tag == "Target")
        {
            // Determine if the player hit the front or back side
            Vector3 hitSide = hit.normal;
            Vector3 objectForward = transform.forward;
            float dotProductSide = Vector3.Dot(hitSide, objectForward);

            // Determine if the player hit the top or bottom side
            Vector3 hitDirection = hit.point - target.transform.position;
            float dotProduct = Vector3.Dot(hitDirection, target.transform.up);

            Debug.Log("Hit, " + dotProduct);

            // Reverse if hit on the other side
            if(dotProductSide > 0) dotProduct *= -1;

            if (dotProduct > 0)
            {
                // Hit on the top side
                Rotate(swingTop);
            }
            else
            {
                // Hit on the bottom side
                Rotate(swingBottom);
            }
        }

        effectScript.Play(hit, hitSound, hitEffect, effectDuration);
    }



    
}
