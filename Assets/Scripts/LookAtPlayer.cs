using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        var rotation = _camera.transform.rotation;
        // Rotate canvas always on top of object
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}
