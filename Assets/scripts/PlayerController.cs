using UnityEngine;
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
    float StartAccelerationY;
    Vector3 movement;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
#if UNITY_ANDROID
        StartAccelerationY = Input.acceleration.y;
#endif
    }
    void FixedUpdate()
    {
#if UNITY_ANDROID
        moveHorizontal = Input.acceleration.x; // Lewo - Prawo
        moveVertical = .15f + Input.acceleration.y;
        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt*2);
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
#endif

#if UNITY_EDITOR
        moveHorizontal = Input.GetAxis("Horizontal");
         moveVertical = Input.GetAxis("Vertical");
        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
#endif
        _rb.velocity = movement * speed;

        _rb.position = new Vector3
        (
            Mathf.Clamp(_rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(_rb.position.z, boundary.zMin, boundary.zMax)
        );
      
        //_rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);
    }
}