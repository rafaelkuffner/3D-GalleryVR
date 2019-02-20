using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    public string cloudConfig;
    public GameObject parent;
    public GameObject hand;

    private CloudVideoPlayer _video;
    // Use this for initialization
    private SteamVR_TrackedObject _trackedobj = null;

    private SteamVR_Controller.Device _controller;

    void Start () {
        _video = new CloudVideoPlayer(cloudConfig,parent);
        _trackedobj = hand.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {
        if (_trackedobj.index == SteamVR_TrackedObject.EIndex.None ) return;
        _controller = SteamVR_Controller.Input((int)_trackedobj.index);

        if (_controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Vector2 touchpad = _leftController.GetAxis(EVRButtonId.k_EButton_Axis0);
            Vector2 touchpad = _controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

            _video.Playing = !_video.Playing;

            if (_video.Playing)
                _video.Play();
            else
                _video.Pause();    
        }
    }
}
