using MyDatatypes.Firearm.Recoil;
using System.Collections;
using UnityEngine;

public class Firearm_Recoil : MonoBehaviour
{
    public bool recoilEnabled { get; set; }
    public FirearmRecoil idleRecoil 
    {
        get { return _idleRecoil; }
        set { _idleRecoil = value; }
    }
    public FirearmRecoil aimingRecoil 
    {
        get { return _aimingRecoil; }
        set { _aimingRecoil = value; }
    }

    [SerializeField] private FirearmRecoil _idleRecoil;
    [SerializeField] private FirearmRecoil _aimingRecoil;

    private RecoilHandler _headRecoilHandler;
    private RecoilHandler _weaponRecoilHandler;

    private Transform _headTransform;

    private void Awake()
    {
        recoilEnabled = true;
    }

    private void OnEnable()
    {
        if (_headRecoilHandler._transform) 
        {
            _headRecoilHandler.ResetRecoilState();
            StartCoroutine(_headRecoilHandler.RecoilRoutine());
        }

        if (_weaponRecoilHandler._transform) 
        {
            _weaponRecoilHandler.ResetRecoilState();
            StartCoroutine(_weaponRecoilHandler.RecoilRoutine());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void InitFirearmRecoil()
    {
        GetHeadTransform();

        if (_headTransform) {
            _headRecoilHandler = new RecoilHandler(_headTransform, _idleRecoil.headRecoil);
        }

        _weaponRecoilHandler = new RecoilHandler(transform, _idleRecoil.weaponRecoil);
    }

    public void IdleFireRecoil() 
    {
        if (!recoilEnabled) return;

        if (_headTransform) _headRecoilHandler.FireRecoil(_idleRecoil.headRecoil.recoilApplied);
        _weaponRecoilHandler.FireRecoil(_idleRecoil.weaponRecoil.recoilApplied);
    }

    public void AimingFireRecoil()
    {
        if (!recoilEnabled) return;

        if (_headTransform) _headRecoilHandler.FireRecoil(_aimingRecoil.headRecoil.recoilApplied);
        _weaponRecoilHandler.FireRecoil(_aimingRecoil.weaponRecoil.recoilApplied);
    }

    private void GetHeadTransform() 
    {
        Transform parent = transform.parent;
        while (parent != null && parent.name != "Head_Recoil") 
        {
            parent = parent.parent;
        }

        _headTransform = parent;
    }


    private struct RecoilHandler
    {
        public Transform _transform;
        public RecoilData _recoil;

        public RecoilAppliedData _targetTransformData;
        public Vector3 _currentEulerAngles;

        public RecoilHandler(Transform transform, RecoilData recoil)
        {
            _transform = transform;
            _recoil = recoil;
            _currentEulerAngles = transform.localEulerAngles;
            _targetTransformData = new RecoilAppliedData(transform);
        }

        public void FireRecoil(RecoilAppliedData recoilApplied)
        {
            _targetTransformData.position += new Vector3(recoilApplied.position.x, recoilApplied.position.y, recoilApplied.position.z);
            _targetTransformData.eulerAngles += new Vector3(recoilApplied.eulerAngles.x, Random.Range(-recoilApplied.eulerAngles.y, recoilApplied.eulerAngles.y), Random.Range(-recoilApplied.eulerAngles.z, recoilApplied.eulerAngles.z));
        }

        public void ResetRecoilState() 
        {
            _transform.localPosition = Vector3.zero;
            _transform.localRotation = Quaternion.identity;
            _currentEulerAngles = Vector3.zero;
            _targetTransformData.SetData(Vector3.zero, Vector3.zero);
        }

        public IEnumerator RecoilRoutine()
        {
            yield return null;
            while (true)
            {
                _targetTransformData.position = Vector3.Lerp(_targetTransformData.position, Vector3.zero, _recoil.recoverySpeed * Time.deltaTime);
                _transform.localPosition = Vector3.Lerp(_transform.localPosition, _targetTransformData.position, _recoil.snappiness * Time.deltaTime);

                _targetTransformData.eulerAngles = Vector3.Lerp(_targetTransformData.eulerAngles, Vector3.zero, _recoil.recoverySpeed * Time.deltaTime);
                _currentEulerAngles = Vector3.Slerp(_currentEulerAngles, _targetTransformData.eulerAngles, _recoil.snappiness * Time.deltaTime);
                _transform.localEulerAngles = _currentEulerAngles;
                yield return null;
            }
        }
    }
}


