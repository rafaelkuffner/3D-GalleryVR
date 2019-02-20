using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCollider : MonoBehaviour {
    
    public GameObject _rightHand;

    private SteamVR_TrackedObject _rightObj = null;
    private SteamVR_Controller.Device _rightController;
    private SteamVR_Controller.Device device;

    GameObject _rig;
    bool mini;
    bool scaling;

    Transform originalDad;
    Vector3 startScale;
    Vector3 startPosition;
    float passedTime;
    public float targetTime = 5;
    // Use this for initialization
    void Start()
    {
        _rig = GameObject.Find("CameraRig");
       _rightObj = _rightHand.GetComponent<SteamVR_TrackedObject>();
       mini = false;
       originalDad = transform.parent;
       passedTime = 0;
    }
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.transform.position;
        Vector3 mypos = transform.position;
        mypos.x = pos.x;
        mypos.z = pos.z;
        this.transform.position = mypos;
        if (scaling)
        {
            AnimateScale();
        }
	}

    void AnimateScale() 
    {
        float delta = Time.deltaTime;
        passedTime += delta;
        float lerp = passedTime / targetTime;
        if (lerp > 1) lerp = 1;
        Vector3 currentScale = Vector3.Lerp(startScale, Vector3.one, lerp);
        Vector3 currentPosition = Vector3.Lerp(startPosition, Vector3.zero, lerp);
        _rig.transform.localScale = currentScale;
        _rig.transform.localPosition = currentPosition;
        if (lerp == 1) scaling = false;
    }

    void StartScale(Transform target)
    {
        _rig.transform.SetParent(target);
        startScale = _rig.transform.localScale;
        startPosition = _rig.transform.localPosition;
        passedTime = 0;
    }

    void OnTriggerStay(Collider other)
    {

        if (other.name == "miniRoom")
        {
            Debug.Log("yes");
            if (_rightObj.index == SteamVR_TrackedObject.EIndex.None) return;
            _rightController = SteamVR_Controller.Input((int)_rightObj.index);

            if (_rightController.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (!mini) { 
                    StartScale(other.transform);
                    mini = true;
                    scaling = true;
                }
                else
                {
                    StartScale(originalDad);
                    mini = false;
                    scaling = true;
                }
            }
        }
    }
}


