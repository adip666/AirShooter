﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    Rigidbody _rb;
    float moveHorizontal;
    float moveVertical;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
             moveHorizontal = Input.GetAxis("Horizontal");
             moveVertical = Input.GetAxis("Vertical");
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor){
            moveHorizontal = -Input.acceleration.y;
             moveVertical = Input.acceleration.x;
        }
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rb.velocity = movement * speed;

        _rb.position = new Vector3
        (
            Mathf.Clamp(_rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(_rb.position.z, boundary.zMin, boundary.zMax)
        );
       // _rb.velocity.Normalize();
        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);
    }
}