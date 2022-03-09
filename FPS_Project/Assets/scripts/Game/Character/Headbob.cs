using MyDatatypes.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    [SerializeField] private HeadbobData _walk;
    [SerializeField] private HeadbobData _sprint;
    [SerializeField] private HeadbobData _crouch;
    [SerializeField] private HeadbobData _prone;

    [Header("Setup")]
    [SerializeField] private FirstPersonController _fpc;

    private float _defaultCamYPos; 
    private float _timer; 
    private float _headbobAmmount; 

    #region Custom Datatype
    [Serializable]
    private struct HeadbobData 
    {
        public float speed;
        public float ammount;
    }
    #endregion

    private void Awake()
    {
        _defaultCamYPos = transform.localPosition.y;
    }

    private void Update()
    {
        HandleHeadbob();
    }

    private void HandleHeadbob()
    {
        if (!_fpc.isGrounded) return;

        if (_fpc.isMoving) 
        {
            GetTimer();
            GetHeadbobAmmount();

            float newYPos = _defaultCamYPos + Mathf.Sin(_timer) * _headbobAmmount;
            transform.transform.localPosition = new Vector3(transform.transform.localPosition.x, newYPos , transform.transform.localPosition.z);
        }
    }

    private void GetHeadbobAmmount()
    {
        switch (_fpc.currentPoseState)
        {
            case PoseState.Standing:
                if (_fpc.isSprinting)
                _headbobAmmount = _sprint.ammount;
                else
                _headbobAmmount = _walk.ammount;
                break;
            case PoseState.Crouching:
                _headbobAmmount = _crouch.ammount;
                break;
            case PoseState.Prone:
                _headbobAmmount = _prone.ammount;
                break;
        }
    }

    private void GetTimer()
    {
        switch (_fpc.currentPoseState) 
        {
            case PoseState.Standing:
                if (_fpc.isSprinting)
                    _timer += _sprint.speed;
                else
                    _timer += _walk.speed;
                break;
            case PoseState.Crouching:
                    _timer += _crouch.speed;
                break;
            case PoseState.Prone:
                    _timer += _prone.speed;
                break;
        }
    }
}
