using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class PlayAnnotation : MonoBehaviour {

    public GameObject[] animatedObjects;
    
	// Use this for initialization
	void Start () {
        VideoPlayer vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += StartAnimation;
        vp.started += StartAnimation;
	}
	
	// Update is called once per frame
    void StartAnimation(VideoPlayer vp)
    {
        foreach(GameObject g in animatedObjects) { 
            Animator a = g.GetComponent<Animator>();
            a.SetTrigger("PlayVideo");
        }
    }
}
