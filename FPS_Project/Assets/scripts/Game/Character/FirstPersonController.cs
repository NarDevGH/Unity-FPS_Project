using MyDatatypes.Movement;
using System;
using System.Collections;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    private const float SLOPE_CHECK_DIST = 2f;
    private const float STANDUP_CHECK_DIST = 1f;

    private const float CLIMBABLE_CHECKBOX_DIST = .5f;
    private readonly Vector3 CLIMBABLE_CHECKBOX_SCALE = new Vector3(.05f, 1f, .1f);
    private const string CLIMBABLE_TAG = "Climbable";

    public bool canMove { get; set; }
    public bool canSprint { get; set; }
    public bool canJump { get; set; }
    public bool canCrouch { get; set; }
    public bool canProne { get; set; }
    public bool canClimb { get; set; }
    public bool canSlide { get; set; }
    public bool canDolphinDive { get; set; }
    public bool canAutoSlideOnSlopes { get; set; }


    public bool isStanding => _currentPoseState == PoseState.Standing;
    public bool isCrouching => _currentPoseState == PoseState.Crouching;
    public bool isProne => _currentPoseState == PoseState.Prone;
    public bool isSprinting => _sprinting;
    public bool isClimbing => _climb;
    public bool isGrounded => _charController.isGrounded;
    public bool isMoving => (canMove && (_moveDirectionInput.x != 0f || _moveDirectionInput.y != 0f));

    public PoseState currentPoseState => _currentPoseState;
    public MovementState currentMovementState {
        get 
        {
            if (_currentMoveSpeed > 0)
            {
                if (_currentMoveSpeed <= _walkSpeed) return MovementState.Walking;
                else return MovementState.Sprinting;            
            }
            else return MovementState.Still;
        }
    }


    [Header("Movement Parameters")]
    [SerializeField, Min(0)] private float _walkSpeed = 3f;
    [SerializeField, Min(0)] private float _sprintSpeed = 6f;
    [SerializeField, Min(0)] private float _crouchSpeed = 2f;
    [SerializeField, Min(0)] private float _proneSpeed = 1f;
    [SerializeField, Min(0), Tooltip("Accelerartion and Desaceleracion")] private float _accDes = 0.1f;
    [SerializeField, Min(0)] private float _jumpForce = 3f;
    [Space]
    [SerializeField, Min(0)] private float _slideSpeed = 10f;
    [SerializeField, Min(0)] private float _slideDuration = 0.5f;
    [Space]
    [SerializeField, Min(0)] private float _dolphinDiveSpeed = 10f;
    [SerializeField, Min(0)] private float _dolphinDiveJump = 0.5f;
    [Space]
    [SerializeField, Min(0)] private float _climbSpeed = 2f;
    [SerializeField, Min(0)] private float _slipSlopeSpeed = 1f;

    [Header("Physics Parameters"), Space]
    [SerializeField, Min(0)] private float _mass = 1f;

    [Header("Look Parameters"), Space]
    [SerializeField, Min(0)] private float _lookSpeed = 3f;
    [SerializeField] private VerticalLookLimit _verticalLookLimit;


    [Header("Poses Parameters"), Space]
    [SerializeField] private float _swapPoseSpeed = 1f;
    [SerializeField] private PoseData _standingData;
    [SerializeField] private PoseData _crouchData;
    [SerializeField] private PoseData _proneData;

    [Header("Setup"), Space]
    [SerializeField] private Transform _head;
    [SerializeField] private CharacterController _charController;

    private Vector3 _moveDirection;
    private Vector2 _moveDirectionInput;
    private float _targetMoveSpeed;
    private float _currentMoveSpeed;

    private Vector2 _lookInput;
    private float xRotation;

    private PoseState _currentPoseState = PoseState.Standing;
    private PoseData _targetPoseData;

    private MovementState _currentMovementState = MovementState.Still;

    private RaycastHit _groundHitPoint;


    private bool _sprinting = false;
    
    private bool _climb => Climbing();
    private bool _sliding => _slideRoutine != null;
    private Coroutine _slideRoutine;
    private bool _dolphinDiving => _dolphinDiveRoutine != null;
    private Coroutine _dolphinDiveRoutine;

    private bool _canStandUp => !Physics.Raycast(_head.transform.position, Vector3.up, STANDUP_CHECK_DIST);
    private bool _overSlope
    {
        get
        {
            if (_charController.isGrounded)
            {
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, SLOPE_CHECK_DIST))
                {
                    _groundHitPoint = hit;
                    Vector3 _groundHitPointNormal = hit.normal;
                    return Vector3.Angle(_groundHitPointNormal, Vector3.up) > _charController.slopeLimit;
                }
            }
            return false;
        }
    }

    #region Custom DataTypes

    

    [Serializable]
    private struct VerticalLookLimit 
    {
        public float lowerLookLimit;
        public float upperLookLimit;
    }
    #endregion

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        canMove = true;
        canSprint = true;
        canJump = true;
        canCrouch = true;
        canProne = true;
        canAutoSlideOnSlopes = true;
        canClimb = true;
        canSlide = true;
        canDolphinDive = true;
    }

    private void Update()
    {
        HandleLook();

        HandleCharController_Height_Center();

        HandleMovement();

        HandleClimbing();

        ApplyFinalMovement();
    }

    public void MovementInput(Vector2 input)
    {
        _moveDirectionInput = input;
    }

    public void LookInput(Vector2 input)
    {
        _lookInput = input;
    }

    public void Jump() 
    {
        if (!canJump) return;

        if (_charController.isGrounded && !_overSlope && !_sliding) 
        {
            _moveDirection.y = _jumpForce;
        }
    }

    #region SPRINT
    public void StartSprint() 
    {
        if (!canSprint || !_canStandUp || _dolphinDiving) return;

        if (_sliding) 
        {
            StopCoroutine(_slideRoutine);
            _slideRoutine = null;
        }

        _currentPoseState = PoseState.Standing;

        _sprinting = true; 
    }

    public void StopSprint() 
    { 
        _sprinting = false; 
    }
    #endregion

    public void SetCurrentStateTowardStanding()
    {
        if (!_canStandUp) return;

        if (_currentPoseState == PoseState.Crouching)
        {
            _currentPoseState = PoseState.Standing;
        }
        else if (_currentPoseState == PoseState.Prone)
        {
            _currentPoseState = PoseState.Crouching;
        }
    }

    public void SetCurrentStateTowardsProning()
    {
        if (_currentPoseState == PoseState.Standing)
        {
            _currentPoseState = PoseState.Crouching;
        }
        else if (_currentPoseState == PoseState.Crouching)
        {
            _currentPoseState = PoseState.Prone;
        }
    }

    public void Slide() 
    {
        if (!canCrouch || !canSlide) return;

        if (_charController.isGrounded && _slideRoutine == null) 
        {
            _slideRoutine = StartCoroutine(SlideRoutine());
        }
    }

    public void DolphinDive() 
    {
        if (_dolphinDiveRoutine != null) return;

        _dolphinDiveRoutine = StartCoroutine(DolphindiveRoutine());
    }

    private void HandleMovement() 
    {
        if (!canMove || _overSlope || _climb || _dolphinDiving) return;

        GetCurrentMoveSpeed();

        float moveX = _moveDirectionInput.x * _currentMoveSpeed;
        float moveZ = _moveDirectionInput.y * _currentMoveSpeed;

        float moveDirection_Y = _moveDirection.y;
        _moveDirection = (transform.TransformDirection(Vector3.forward) * moveZ) + (transform.TransformDirection(Vector3.right) * moveX);
        _moveDirection.y = moveDirection_Y;

    }

    private void GetCurrentMoveSpeed() 
    {
        GetTargetSpeed();

        if(_currentMoveSpeed != _targetMoveSpeed) 
        {
            _currentMoveSpeed = Mathf.MoveTowards(_currentMoveSpeed, _targetMoveSpeed, _accDes);
        }
    }

    private void GetTargetSpeed()
    {
        switch (_currentPoseState)
        {
            case PoseState.Standing:
                if (_sprinting) _targetMoveSpeed = _sprintSpeed;
                else _targetMoveSpeed = _walkSpeed;
                break;

            case PoseState.Crouching:
                _targetMoveSpeed = _crouchSpeed;
                break;

            case PoseState.Prone:
                _targetMoveSpeed = _proneSpeed;
                break;
        }
    }


    private void HandleLook()
    {
        xRotation -= _lookInput.y * _lookSpeed;
        float yRotation = _lookInput.x * _lookSpeed;

        xRotation = Mathf.Clamp(xRotation, _verticalLookLimit.lowerLookLimit, _verticalLookLimit.upperLookLimit);

        _head.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, yRotation, 0f) ;
    }


    private void ApplyFinalMovement() 
    {
        if (!_charController.isGrounded && !_climb)
        {
            _moveDirection.y -= -Physics.gravity.y * _mass * Time.deltaTime;
        }
        else 
        {
            if (canAutoSlideOnSlopes && _overSlope) 
            {
                Vector3 _groundHitPointNormal = _groundHitPoint.normal;
                _moveDirection += new Vector3(_groundHitPointNormal.x, -_groundHitPointNormal.y, _groundHitPointNormal.z) * _slipSlopeSpeed;
            }
        }
        _charController.Move(_moveDirection * Time.deltaTime);
    }

    private void HandleCharController_Height_Center() 
    {
        GetTargetPoseData();

        if (_charController.height != _targetPoseData.height) 
        {
            _charController.height = Mathf.MoveTowards(_charController.height, _targetPoseData.height, _swapPoseSpeed);
        }
        if (_charController.center != _targetPoseData.center) 
        {
            _charController.center = Vector3.MoveTowards(_charController.center, _targetPoseData.center, _swapPoseSpeed);
        }
    }

    private void GetTargetPoseData()
    {
        switch (_currentPoseState)
        {
            case PoseState.Standing:
                _targetPoseData = _standingData;
                break;

            case PoseState.Crouching:
                _targetPoseData = _crouchData;
                break;

            case PoseState.Prone:
                _targetPoseData = _proneData;
                break;
        }
    }

    private bool Climbing() 
    {
        if (_charController.isGrounded && _moveDirectionInput.y < 0)
            return false;

        Collider[] insideCheckBox = Physics.OverlapBox(transform.position + (transform.forward * CLIMBABLE_CHECKBOX_DIST), CLIMBABLE_CHECKBOX_SCALE, Quaternion.LookRotation(_head.forward));
        foreach (Collider collision in insideCheckBox) 
        {
            if (collision.CompareTag(CLIMBABLE_TAG)) 
            {
                return true;
            }
        }
        return false;
    }

    private void HandleClimbing()
    {
        if (_climb)
        {
            if (_currentPoseState == PoseState.Prone) return;

            if (_currentPoseState == PoseState.Crouching) _currentPoseState = PoseState.Standing;

            _moveDirection = new Vector3(0f, _climbSpeed * _moveDirectionInput.y, 0f);
        }
    }

    private IEnumerator SlideRoutine()
    {
        _currentPoseState = PoseState.Crouching;

        float _slideTimer = 0.0f;
        Vector3 _slideDir = transform.forward;

        while (_slideTimer < _slideDuration)
        {
            _slideTimer += Time.deltaTime;
            _charController.Move(_slideDir * _slideSpeed * Time.deltaTime);
            yield return null;
        }

        _slideRoutine = null;
    }

    private IEnumerator DolphindiveRoutine()
    {
        _currentPoseState = PoseState.Prone;

        _moveDirection.y = _dolphinDiveJump;

        Vector3 _dolphinDiveDir = transform.forward;

        while (!_charController.isGrounded || _moveDirection.y > 0) // it may not enter in the loop bc its still reg.isGrounded, thats why it checks Jump Force, to only break when the player start falling
        {
            _charController.Move(_dolphinDiveDir * _dolphinDiveSpeed * Time.deltaTime);
            yield return null;
        }

        _dolphinDiveRoutine = null;
    }
}
