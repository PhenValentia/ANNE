using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour // Entirely by NDS8
{
    void OnTriggerEnter(Collider col) // On entering a collider, test if the player is that collider. If so, send to the killed screen.
    {
        if (col.tag == "Player")
        {
            Debug.Log("TouchedPlayer3");
            SceneManager.LoadScene("Killed");
        }
    }
}
