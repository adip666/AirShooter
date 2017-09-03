using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    
    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Invoke("Hide", 5);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
