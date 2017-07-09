using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControll : GameController {


    public float speed;
    public Transform target;
    public Vector3 SpawnPosition;
   
        void Awake()
    {
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, target.position.z), step);
    }
    public void SpownBackground()
    {
        transform.position = SpawnPosition;
    }
}
