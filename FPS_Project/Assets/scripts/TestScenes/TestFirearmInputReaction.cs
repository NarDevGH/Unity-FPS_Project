using UnityEngine;
using MyDatatypes.Movement;

public class TestFirearmInputReaction : MonoBehaviour
{
    [SerializeField] private GameObject _currentWeapon;

    [Space,Header("Movement")]
    [SerializeField] private PoseState _currentPoseState;
    [SerializeField] private MovementState _currentMovementState;
    private PoseState _lastPoseState;
    private MovementState _lastMovementState;

    [Space, Header("Weapon")]
    [SerializeField] private bool _aiming;
    [SerializeField] private bool _firing;
    private bool _detectAimingChange;
    private bool _detectFiringChange;


    private IControllFireArm _firearmController;


    private void Awake()
    {
        _lastMovementState = _currentMovementState;
        _lastPoseState = _currentPoseState;
        _detectAimingChange = _aiming;
        _detectFiringChange = _firing;
        if(_currentWeapon != null) _firearmController = _currentWeapon.GetComponent<IControllFireArm>();
    }

    private void Update()
    {
        HandlePoseState();

        HandleMovementState();

        HandleAiming();

        HandleFiring();
    }
    

    public void FireWeapon() 
    {
        _firearmController.StartFireWeapon();
    }

    public void RealodWeapon() 
    {
        _firearmController.ReloadWeapon();
    }

    public void NextWeaponAds() 
    {
        _firearmController.NextAdsStateWeapon();
    }


    private void HandleFiring()
    {
        if (_firing && _firing != _detectFiringChange)
        {
            _detectFiringChange = _firing;

            _firearmController.StartFireWeapon();
        }
        else if (_firing != _detectFiringChange)
        {
            _detectFiringChange = _firing;

            _firearmController.StopFireWeapon();
        }
    }

    private void HandleAiming()
    {
        if (_aiming && _aiming != _detectAimingChange)
        {
            _detectAimingChange = _aiming;

            _firearmController.StartAdsWeapon();
        }
        else if (_aiming != _detectAimingChange)
        {
            _detectAimingChange = _aiming;

            _firearmController.StopAdsWeapon();
        }
    }

    private void OnPoseStateChange() 
    {
        switch (_currentPoseState) 
        {
            case PoseState.Standing:
                break;
            case PoseState.Crouching:
                break;
            case PoseState.Prone:
                break;
        }
    }
    private void HandlePoseState()
    {
        if (_lastPoseState != _currentPoseState)
        {
            _lastPoseState = _currentPoseState;
            OnPoseStateChange();
        }
    }

    private void OnMovementStateChange() 
    {
        switch (_currentMovementState) 
        {
            case MovementState.Still:
                break;
            case MovementState.Walking:
                break;
            case MovementState.Sprinting:
                break;

        }
    }
    private void HandleMovementState()
    {
        if (_lastMovementState != _currentMovementState)
        {
            _lastMovementState = _currentMovementState;
            OnMovementStateChange();
        }
    }

}
