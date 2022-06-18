using UnityEngine;
using MyDatatypes.Movement;

public class Weapon_MovementSway : MonoBehaviour
{
    [SerializeField] private AnimationCurve _swayPattern;
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private str_SwayMod _still;
    [SerializeField] private str_SwayMod _walking;
    [SerializeField] private str_SwayMod _sprinting;


    private MovementState _currentMovementState
    {
        get 
        {
            if (PlayerInput.MoveDir_Input != Vector2.zero) 
            {
                if (PlayerInput.Sprinting) return MovementState.Sprinting;
                else return MovementState.Walking;
            }
            return MovementState.Still;
        }
    }

    private Vector2 _currentMultiplier;
    private Vector2 _targetMultiplier;
    private float _currentSpeed;
    private float _targetSpeed;

    private MovementState _oldSwayState;

    private float _curveDuration;
    private float _curveCurrentTime;


    [System.Serializable]
    private struct str_SwayMod
    {
        public Vector2 multiplier;
        public float speed;
    }




    private void Awake()
    {
        SetTargets();
        _currentMultiplier = _targetMultiplier;
        _currentSpeed = _targetSpeed;

        _oldSwayState = _currentMovementState;

        _curveCurrentTime = 0f;
        _curveDuration = _swayPattern[_swayPattern.length - 1].time;
    }


    private void Update()
    {
        SetTargets();

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _targetSpeed, _transitionSpeed * Time.deltaTime);
        _currentMultiplier = Vector2.MoveTowards(_currentMultiplier, _targetMultiplier, _transitionSpeed * Time.deltaTime);


        float curveValue = _swayPattern.Evaluate(AvoidNaN(_curveCurrentTime));
        Vector3 endPos = new Vector3(curveValue * _currentMultiplier.x, -curveValue * _currentMultiplier.y, 0f);
        transform.localPosition = endPos;


        _curveCurrentTime += (Time.deltaTime * _currentSpeed);
        if (_curveCurrentTime > _curveDuration)
        {
            _curveCurrentTime = 0f;
        }
    }


    private float AvoidNaN(float value)
    {
        return value > 0.001 ? value : 0f;
    }

    private void SetTargets()
    {
        if (PlayerInput.Aiming_Input) 
        {
            _targetMultiplier = Vector2.zero;
            _targetSpeed = 0;
            return;
        }

        switch (_currentMovementState)
        {
            case MovementState.Still:
                _targetMultiplier = _still.multiplier;
                _targetSpeed = _still.speed;
                break;
            case MovementState.Walking:
                _targetMultiplier = _walking.multiplier;
                _targetSpeed = _walking.speed;
                break;
            case MovementState.Sprinting:
                _targetMultiplier = _sprinting.multiplier;
                _targetSpeed = _sprinting.speed;
                break;
        }
    }
}