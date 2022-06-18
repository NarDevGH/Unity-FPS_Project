using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_MovementLean : MonoBehaviour
{
    [SerializeField] private float _leanSpeed;
    [SerializeField] private Vector3 _leanAngles;

    private Vector3 _TargetEulerAngles;
    private Vector3 _currentEulerAngles;

    private void Update()
    {
        if (PlayerInput.Aiming_Input) //Dont lean weapon when aiming
        {
            _TargetEulerAngles = Vector3.zero;
        }
        else 
        {
            GetTargetEulerAngles();
        }

        _currentEulerAngles = Vector3.Slerp(_currentEulerAngles, _TargetEulerAngles, _leanSpeed * Time.deltaTime);
        transform.localEulerAngles = _currentEulerAngles;
    }

    private void GetTargetEulerAngles()
    {
        if (PlayerInput.Horizontal_Input > 0)
        {
            _TargetEulerAngles.z = -_leanAngles.z;
        }
        else if (PlayerInput.Horizontal_Input < 0)
        {
            _TargetEulerAngles.z = _leanAngles.z;
        }
        else 
        {
            _TargetEulerAngles.z = 0f;
        }

        if (PlayerInput.Vertical_Input > 0)
        {
            _TargetEulerAngles.x = _leanAngles.x;
        }
        else if (PlayerInput.Vertical_Input < 0)
        {
            _TargetEulerAngles.x = -_leanAngles.x;
        }
        else
        {
            _TargetEulerAngles.x = 0f;
        }
    }
}
