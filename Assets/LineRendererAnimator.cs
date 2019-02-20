using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererAnimator : MonoBehaviour {

    public float duration;
    public bool animate;
    public bool reset;

    LineRenderer _line;
    // Use this for initialization
    Vector3[] _positions;
    private float _count;
    private int _position;
    private float _totalDistance;

	void Start () {
        _line = this.GetComponent<LineRenderer>();
        _positions = new Vector3[_line.positionCount];
        _line.GetPositions(_positions);
        _totalDistance = 0;
        for (int i = 1; i < _line.positionCount;i++)
        {
           _totalDistance += Vector3.Distance(_positions[i], _positions[i - 1]);
        }
        _line.positionCount = 2;
        _line.SetPosition(0, _positions[0]);
        _line.SetPosition(1, _positions[0]);
        _position = 1;
        _count = 0;
        reset = false;
	}

    void Reset()
    {
        _line.positionCount = 2;
        _line.SetPosition(0, _positions[0]);
        _line.SetPosition(1, _positions[0]);
        _position = 1;
        _count = 0;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        if (reset)
        {
            Reset();
            reset = false;
        }
        if (animate && _position < _positions.Length)
        {

            Vector3 a = _positions[_position-1];
            Vector3 b = _positions[_position];
            float d = Vector3.Distance(a, b);
            float ratio = d / _totalDistance;
            float segmentTime = ratio * duration;
            _count += Time.fixedDeltaTime;

            if(_count <= segmentTime) { 
                float timeRatio = _count / segmentTime;
                Vector3 dir = b - a;
                Vector3 pos = a + (timeRatio) *dir;
                _line.SetPosition(_position, pos);
            }else
            {
                _line.SetPosition(_position, b);
                _position++;
                _count = 0;
                if(_position == _positions.Length)
                {
                    animate = false;
                }
                else
                {
                _line.positionCount++;
                _line.SetPosition(_position, b);
                }
            }

        }
    }
}
