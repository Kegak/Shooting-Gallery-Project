using Unity.VisualScripting;
using UnityEngine;

public class TargetCube : Target
{
    bool left;
    bool rotationOn = false;
    float rotationLeft = 360f;
    float rotationRight = -360;
    float currentRotationAmount = 0f;

    Vector3 rotation;

    float time;

    void Update()
    {
        if (rotationOn)
        {
            if(left)
            {
                if (currentRotationAmount < rotationLeft)
                {
                    time = Time.deltaTime;
                    target.transform.Rotate(rotation * time);
                    currentRotationAmount += rotationLeft * time;
                }
                else
                {
                    rotationOn = false;
                    target.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else
            {
                Debug.Log("Current Rotation: " + currentRotationAmount);
                if (-currentRotationAmount > rotationRight)
                {
                    time = Time.deltaTime;
                    target.transform.Rotate(rotation * time);
                    currentRotationAmount -= rotationRight * time;
                }
                else
                {
                    rotationOn = false;
                    target.transform.rotation = Quaternion.Euler(0, 0, 0);
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
                left = true;
                rotationLeft = degrees;
            }
            else 
            {
                left = false;
                rotationRight = degrees;
            }
            rotationOn = true;
            currentRotationAmount = 0f;
            rotation = new Vector3(0, degrees, 0);
        }
    }

    public override void Process(RaycastHit hit)
    {
        if (gameObject.tag == "Target")
        {
            // Determine the hit side based on the hit point
            Vector3 hitDirection = hit.point - target.transform.position;
            float dotProduct = Vector3.Dot(hitDirection, target.transform.right);

            Debug.Log("Hit, " + dotProduct);

            if (dotProduct > 0)
            {
                // Hit on the right side, rotate right
                Rotate(rotationRight);
            }
            else
            {
                // Hit on the left side, rotate left
                Rotate(rotationLeft);
            }
        }

        effectScript.Play(hit, hitSound, hitEffect, effectDuration);
    }
}
