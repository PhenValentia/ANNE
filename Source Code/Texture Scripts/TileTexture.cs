using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTexture : MonoBehaviour // Entirely by NDS8
{
    [SerializeField] private float tileX = 1;
    [SerializeField] private float tileY = 1;
    Mesh mesh;
    private Material mat;
    
    // Make the texture of an object fit a certain tiling.
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mesh = GetComponent<MeshFilter>().mesh;

    }
    
    // Make the texture of an object fit a certain tiling.
    void Update()
    {
        mat.mainTextureScale = new Vector2((mesh.bounds.size.x *
        transform.localScale.x) / 100 * tileX, (mesh.bounds.size.y * transform.localScale.y) / 100 * tileY);
    }
}