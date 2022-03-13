using MyDatatypes.Firearm.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm_Ads : MonoBehaviour
{
    private const float FIX_FOV_SPEED = 15f;
    private const float FIX_POS_SPEED = 0.1f;
    private const float FIX_ROT_SPEED = 15f;

    #region PUBLIC_VARIABLES
    public AdsData idleState {
        get { return _idleState; }
        set { _idleState = value; }
    }
    public List<AdsData> opticAdsStates {
        get { return _opticAdsStates; }
        set { _opticAdsStates = value; }
    }
    public AdsData altAdsState { 
        get { return _altAdsState; }
        set { _altAdsState = value; }
    }
    public Camera weaponCamera {
        get { return _weaponCamera; }
        set { _weaponCamera = value; }
    }
#endregion
    
    [SerializeField] private AdsData _idleState;
    [SerializeField] private List<AdsData> _opticAdsStates;
    [SerializeField] private AdsData _altAdsState;

    private AdsData _currentAdsState;
    private AdsData _currentIdleState;
    private AdsData[] _opticAdsStatesArray;
    private Camera _weaponCamera;

    public void ResetWeaponCamPos() 
    {
        _weaponCamera.fieldOfView = _currentIdleState.fov;
        _weaponCamera.transform.localPosition = Vector3.zero;
        _weaponCamera.transform.localEulerAngles = Vector3.zero;
    } 


    private void OnEnable()
    {
        ResetWeaponCamPos();
    }

    #region INIT_FIREARM_ADS_METHODS

    public void InitAdsHandler()
    {
        GetWeaponCamera();
        InitCurrentIdleState();

        if (_opticAdsStates.Count > 0)
        {
            GetAdsDisplacementsOnArray();
            InitCurrentAdsState();
        }
        if (_altAdsState.posTransform) 
        {
            _altAdsState.displacement = Ads_Displacement(weaponCamera.transform, _altAdsState.posTransform);
        }
    }
    private void InitCurrentAdsState()
    {
        if (_opticAdsStatesArray.Length > 0) _currentAdsState = _opticAdsStatesArray[0];
    }

    private void InitCurrentIdleState()
    {
        _currentIdleState = _idleState;
    }


    public AdsDisplacement Ads_Displacement(Transform camTransform, Transform adsTransform)
    {
        AdsDisplacement displacement;

        displacement.position_Displacement = adsTransform.position - camTransform.position;
        displacement.eulerAngles = adsTransform.localEulerAngles;

        return displacement;
    }


    private void GetAdsDisplacementsOnArray()
    {
        _opticAdsStatesArray = _opticAdsStates.ToArray();

        for (int i = 0; i < _opticAdsStatesArray.Length; i++)
        {
            _opticAdsStatesArray[i].displacement = Ads_Displacement(weaponCamera.transform, _opticAdsStatesArray[i].posTransform);
        }
    }

    private void GetWeaponCamera()
    {
        var cam = FindObjectsOfType<Camera>(true);
        for (int i = 0; i < cam.Length; i++)
        {
            if (cam[i].name == "Weapon_Camera")
            {
                weaponCamera = cam[i];
            }

        }
    }

    #endregion

    public void NextAdsState()
    {
        if (_altAdsState.posTransform) // if has assigned an _altAdsState
        {
            if (_currentAdsState.posTransform != _altAdsState.posTransform) // if not aiming with _altAdsState
            {
                int index = System.Array.IndexOf(_opticAdsStatesArray, _currentAdsState);
                if (index + 1 == _opticAdsStatesArray.Length) // if reach end of opticAdsStates, use _altAdsState next
                {
                    _currentAdsState = _altAdsState;
                }
                else //else use next opticAdsStates
                {
                    _currentAdsState = _opticAdsStatesArray[index + 1];
                }
            }
            else // if aiming with _altAdsState, use first _opticAdsStatesArray next
            {
                _currentAdsState = _opticAdsStatesArray[0];
            }
        }
        else 
        {
            int index = System.Array.IndexOf(_opticAdsStatesArray, _currentAdsState);
            if (index + 1 == _opticAdsStatesArray.Length)
            {
                _currentAdsState = _opticAdsStatesArray[0];
            }
            else
            {
                _currentAdsState = _opticAdsStatesArray[index + 1];
            }
        }
    }

    public void StartMoveTowardsIdleStateRoutine()
    {
        StopAllCoroutines();

        StartCoroutine(ChangeStateRoutine(_currentIdleState));
    }

    public void StartAdsRoutine() 
    {
        StopAllCoroutines();

        StartCoroutine(ChangeStateRoutine(_currentAdsState));
    }

    private IEnumerator ChangeStateRoutine(AdsData targetState)
    {
            Coroutine fovRoutine = StartCoroutine(ReachTargetFovRoutine(targetState));
            Coroutine posRoutine = StartCoroutine(ReachTargetPosRoutine(targetState));
            Coroutine rotRoutine = StartCoroutine(ReachTargerRotationRoutine(targetState));
            yield return fovRoutine;
            yield return posRoutine;
            yield return rotRoutine;
            StopAllCoroutines();
    }

    private IEnumerator ReachTargetFovRoutine(AdsData targetState) 
    {
        while (weaponCamera.fieldOfView > targetState.fov ? weaponCamera.fieldOfView > targetState.fov : weaponCamera.fieldOfView < targetState.fov)
        {
            weaponCamera.fieldOfView = Mathf.MoveTowards(weaponCamera.fieldOfView, targetState.fov, targetState.swapSpeed * FIX_FOV_SPEED * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator ReachTargetPosRoutine(AdsData targetData) 
    {
        while (Vector3.Distance(weaponCamera.transform.localPosition, targetData.displacement.position_Displacement) > 0f)
        {
            weaponCamera.transform.localPosition = Vector3.MoveTowards(weaponCamera.transform.localPosition, targetData.displacement.position_Displacement, targetData.swapSpeed * FIX_POS_SPEED* Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator ReachTargerRotationRoutine(AdsData targetData) 
    {
        while (true)
        {
            weaponCamera.transform.localRotation = Quaternion.RotateTowards(weaponCamera.transform.localRotation,Quaternion.Euler(targetData.displacement.eulerAngles),targetData.swapSpeed * FIX_ROT_SPEED * Time.deltaTime);
            yield return null;
        }
    }


}
