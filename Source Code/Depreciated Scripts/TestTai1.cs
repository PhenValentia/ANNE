using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTai1 : MonoBehaviour { // by JB2051

    public bool outputTest;
    public Tai timeSystem;

    void Start () {
        outputTest = false;
    }
	
	void Update () {
        if (outputTest == true)
        {
            timeSystem = new Tai(90.0f, 30.0f, 70.0f);
            Debug.Log(timeSystem.getTime());
            outputTest = false;
        }
    }
}
