using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollider : MonoBehaviour {

    GameObject _rig;
	// Use this for initialization
	void Start () {
        _rig = GameObject.Find("CameraRig");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.transform.position;
        Vector3 mypos = transform.position;
        mypos.x = pos.x;
        mypos.z = pos.z;
        this.transform.position = mypos;

        Vector3 posrig = _rig.transform.position;
        posrig.y = mypos.y;
        _rig.transform.position = posrig;
      
    }
}
