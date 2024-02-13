using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class skyboxRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateSpeed;
    private Material skyboxMaterial;
    
    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        if (skyboxMaterial != null)
        {
            float rotation = Time.time * rotateSpeed;
            skyboxMaterial.SetFloat("_Rotation", rotation);
        }
    }
}
