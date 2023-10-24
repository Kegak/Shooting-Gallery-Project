using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f;

    public ParticleSystem muzzleFlash;

    public Vector3 upRecoil = new Vector3(-20, 10, 0);

    Vector3 originalRotation;

    private Camera fpsCamera;
    private float nextTimeToFire;

    void Start()
    {
        fpsCamera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        nextTimeToFire = 0.0f;
        originalRotation = transform.localEulerAngles;
    }


    void Update()
    {
        bool ready = Time.time > nextTimeToFire;

        if (ready && Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            stopRecoil();
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.Process(hit);
            }

            nextTimeToFire = Time.time + 1f;
        }

        Recoil();
    }

    void Recoil()
    {
        transform.localEulerAngles += upRecoil;
    }
    void stopRecoil()
    {
        transform.localEulerAngles = originalRotation;
    }
}
