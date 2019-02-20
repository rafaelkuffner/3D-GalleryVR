using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Teleporter : MonoBehaviour {

    public GameObject _rightHand;
    public GameObject _faceQuad;

    private SteamVR_TrackedObject _rightObj = null;
    private SteamVR_Controller.Device _rightController;
    private SteamVR_Controller.Device device;
    
        //Laser Pointer Variables
    private GameObject _rightHolder;
    private GameObject _rightPointer;
    private float _pointerThickness = 0.005f;
    private Color _pointerColor;

	private string _representation;

    public Color PointerColor
    {
        get { return _pointerColor; }
        set { _pointerColor = value; }
    }

    
    void setupRightPointer()
    {
        _rightHolder = new GameObject();
        _rightHolder.transform.parent = _rightHand.transform;
        _rightHolder.transform.localPosition = Vector3.zero;
        _rightHolder.transform.localRotation = Quaternion.identity;

        _rightPointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _rightPointer.transform.parent = _rightHolder.transform;
        _rightPointer.transform.localScale = new Vector3(_pointerThickness, _pointerThickness, 100f);
        _rightPointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        _rightPointer.transform.localRotation = Quaternion.identity;
        BoxCollider collider = _rightPointer.GetComponent<BoxCollider>();
        collider.isTrigger = true;
        Rigidbody rigidBody = _rightPointer.AddComponent<Rigidbody>();
        rigidBody.isKinematic = true;

        _pointerColor = new Color(0.2f, 0.2f, 0.2f);
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", _pointerColor);
        _rightPointer.GetComponent<MeshRenderer>().material = newMaterial;
    }

    // Use this for initialization

	void Start () {

        _rightObj = _rightHand.GetComponent<SteamVR_TrackedObject>();
        setupRightPointer();
        DisableRightPointer();
  
	}

    void EnableRightPointer()
    {
        _rightPointer.SetActive(true);
    }

    void DisableRightPointer()
    {
        _rightPointer.SetActive(false);
    }

    IEnumerator Teleport(GameObject rig, Transform parent, Vector3 localPosition)
    {
        yield return new WaitForSeconds(0.125f);
        rig.transform.SetParent(parent);
        rig.transform.localPosition = localPosition;
    }
	// Update is called once per frame
	void Update () {

        if (_rightObj.index == SteamVR_TrackedObject.EIndex.None) return;
        
        _rightController = SteamVR_Controller.Input((int)_rightObj.index);


        if (_rightController.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            EnableRightPointer();
        }
        if (_rightController.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Ray raycast = new Ray(_rightHand.transform.position, _rightHand.transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);
            if (bHit )
            {
                Transform daddy = hit.collider.gameObject.transform.parent;
                if(daddy != null && daddy.name.Equals("Room")){
                    GameObject camera = GameObject.Find("[CameraRig]");
                    Vector3 pos = camera.transform.localPosition;
                    StartCoroutine(Teleport(camera, hit.collider.gameObject.transform.parent, pos));
                    if (_faceQuad != null)
                    {
                        Animator a = _faceQuad.GetComponent<Animator>();
                        a.SetTrigger("Animate");
                    }
                }
            }
            DisableRightPointer();
        }
	}


}
