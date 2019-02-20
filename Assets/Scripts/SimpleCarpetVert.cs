using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SimpleCarpetVert : MonoBehaviour {


    public GameObject _rightHand;
    private SteamVR_TrackedObject _rightObj = null;
    private SteamVR_Controller.Device _rightController;
    private SteamVR_Controller.Device device;

	// Use this for initialization
	void Start () {
        _rightObj = _rightHand.GetComponent<SteamVR_TrackedObject>();

	}
	

	// Update is called once per frame
	void Update () {
        
        if (_rightObj.index == SteamVR_TrackedObject.EIndex.None) return;
        _rightController = SteamVR_Controller.Input((int)_rightObj.index);
        if (_rightController.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = _rightController.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            
            if (touchpad.y > 0.5f)
            {
                transform.Translate(new Vector3(0, 2, 0) * Time.deltaTime);
            }
            else if (touchpad.y < -0.5f)
            {
                transform.Translate(new Vector3(0, -2, 0) * Time.deltaTime);
            }
        }  
	}
}
