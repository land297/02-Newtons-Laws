using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbit3 : MonoBehaviour
{
    [SerializeField] Transform _Target;
    [SerializeField] float _XSpeed = 250f;
    [SerializeField] float _YSpeed = 120f;
    [SerializeField] float _YMinLimit = -20f;
    [SerializeField] float _YMaxLimit = 80f;
    [SerializeField] float _Distance = 10f;
    [SerializeField] float _SpeedDampning = 0.02f;
    [SerializeField] float _DistanceDampning = 0.2f;

    float _xAngle, _yAngle;
    // Start is called before the first frame update
    void Start()
    {
        var angles = transform.eulerAngles;
        _xAngle = angles.x;
        _yAngle = angles.y;

        if (GetComponent<Rigidbody>() != null) {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        if(_Target != null) {
            Rotate();
        }
    }

    private void LateUpdate() {
        if (_Target != null) {
            Rotate();
        }
    }

    private void Rotate() {
        _xAngle += Input.GetAxis("Mouse X") * _XSpeed * _SpeedDampning;
        _yAngle += Input.GetAxis("Mouse Y") * _YSpeed * _SpeedDampning;
        _Distance += Input.mouseScrollDelta.y * _DistanceDampning;

        _yAngle = ClampAngle(_yAngle, _YMinLimit, _YMaxLimit);

        var rotation = Quaternion.Euler(_yAngle, _xAngle, 0f);
        var position = rotation * new Vector3(0f, 0f, -_Distance) + _Target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    private float ClampAngle(float angle, float min, float max) {
        if (angle< -360) {
            angle += 360;
        } else if (angle > 360) {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);   
    }
}
