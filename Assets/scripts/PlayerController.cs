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
    public SimpleTouchPad touchPad;
    public TouchAreaButton areaButton;

    bool _touch= false;
    Rigidbody _rb;
    float moveHorizontal;
    float moveVertical;
    float StartAccelerationY;
    Vector3 movement;
    Quaternion calibrationQuaternion;

    public GameObject shot;
    public Transform shotSpawn, shotSpawn2;
    public float fireRate;
    float nextFire;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
#if UNITY_ANDROID
        //StartAccelerationY = Input.acceleration.y;
        CalibrateAccelerometer();
#endif
    }
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshoot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), accelerationSnapshoot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion* acceleration;
        return fixedAcceleration;
    }

    void FixedUpdate()
    {
#if UNITY_ANDROID
        //moveHorizontal = Input.acceleration.x; // Lewo - Prawo
        //moveVertical = .15f + Input.acceleration.y;
        //movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (!_touch)
        {
            Vector3 accelerationRaw = Input.acceleration;
            Vector3 acceleration = FixAcceleration(accelerationRaw);
            movement = new Vector3(acceleration.x, 0.0f, acceleration.y);
           // _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt * 2);
            _rb.velocity = movement * speed;

        }
        else
        {
            Vector2 direction = touchPad.GetDirection();
            movement = new Vector3(direction.x, 0.0f, direction.y);
            //_rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);
            _rb.velocity = movement * speed/2;

        }
        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt * 2);



#endif
        #region sterowanie PC
#if UNITY_EDITOR
        moveHorizontal = Input.GetAxis("Horizontal");
         moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);
        _rb.velocity = movement * speed / 2;
#endif
#endregion
        

        _rb.position = new Vector3
        (
            Mathf.Clamp(_rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(_rb.position.z, boundary.zMin, boundary.zMax)
        );
      
        //_rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);
    }

    public void ChangeSteering()
    {
        _touch = !_touch;
    }
    void Update()
    {
        if (areaButton.CanFire() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);


            // audio.Play();
        }
    }
}