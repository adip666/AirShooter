using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjects : MonoBehaviour {

	void OnTriggerExit(Collider _coll)
    {
        _coll.gameObject.GetComponent<BackgroundControll>().SpownBackground();
    }
}
