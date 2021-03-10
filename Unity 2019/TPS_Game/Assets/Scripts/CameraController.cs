using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camSmoothingFactor = 1;
    public float lookUpMax = 60;
    public float lookUpMin = -60;
    private Quaternion camRotation;

    void Start()
    {
        camRotation = transform.localRotation;
    }

    void Update()
    {
        camRotation.x += Input.GetAxis("Mouse Y") * camSmoothingFactor * -1;
        camRotation.y += Input.GetAxis("Mouse X") * camSmoothingFactor;

        camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);
        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
    }
}
