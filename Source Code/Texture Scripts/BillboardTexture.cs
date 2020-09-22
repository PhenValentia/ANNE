using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardTexture : MonoBehaviour{ // by NDS8

    [SerializeField]
    float modX = 0f;
    [SerializeField]
    float modY = 1f;
    [SerializeField]
    float modZ = 0f;
    [SerializeField]
    float modW = 1f;

    // Makes the texture or object always face the player
    void Update () {
        this.transform.rotation = new Quaternion(Camera.main.transform.rotation.x * modX, Camera.main.transform.rotation.y * modY, Camera.main.transform.rotation.z * modZ, Camera.main.transform.rotation.w * modW);
    }
}
