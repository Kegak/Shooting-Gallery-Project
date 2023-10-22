using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameHider : MonoBehaviour
{
    [Header("This object will be invisible")]
    Renderer test;
    // Start is called before the first frame update
    void Start()
    {
        //Makes the object INVISIBLE in game mode (if FALSE)
        test = GetComponent<MeshRenderer>();
        test.enabled= false;
    }

    //Update called once per frame
    void Update()
    {

    }
}
