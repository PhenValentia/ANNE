using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterAnimation : MonoBehaviour // by JB2051
{
    Animator anim;
    public bool closed;
    void Start()
    {
        anim = GetComponent<Animator>();
        closed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonDown("Light") && closed)
        {
            anim.SetBool("Closed", false);
            this.closed = false;
        }
        else if (UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonDown("Light") && !closed)
        {
            anim.SetBool("Closed", true);
            this.closed = true;
        }
    }
}
