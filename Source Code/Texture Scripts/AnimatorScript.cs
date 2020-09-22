using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour // by JB2051
{
    Animator anim;
    private bool inventorytoggle;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() //many different checks for actions the user is doing, most of this is worked out in the animator.
    {
        float move = Input.GetAxis("Vertical");
        if (inventorytoggle)
        {
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Interact", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Interact", false);
        }
        else
        {
            anim.SetFloat("Speed", move);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("Crouch", true);
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool("Crouch", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Run", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Jump", false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleInventory();
        }
    }

    private void toggleInventory()
    {
        inventorytoggle = !inventorytoggle;
    }
}
