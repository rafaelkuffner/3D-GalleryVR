using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTexture : MonoBehaviour {

    public Texture2D image;
	// Use this for initialization
	void Start () {
       Renderer r =  this.transform.GetComponent<Renderer>();
       r.material.mainTexture = image;
    }

}
