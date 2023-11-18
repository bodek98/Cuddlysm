using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Quaternion _cameraRotation;
    
    void Start()
    {
        _cameraRotation = Camera.main.transform.rotation;
    }
    
    void Update()
    {
        transform.rotation = _cameraRotation;
    }
}
