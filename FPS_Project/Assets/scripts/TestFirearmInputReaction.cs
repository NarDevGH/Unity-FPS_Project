using UnityEngine;
using MyDatatypes.Movement;

public class TestFirearmInputReaction : MonoBehaviour
{
    [SerializeField] private GameObject currentWeapon;

    [Space,Header("Movement")]
    [SerializeField] private PoseState currentPoseState;
    [SerializeField] private MovementState currentMovementState;

    [Space, Header("Weapon")]
    [SerializeField] private bool aiming;
    [SerializeField] private bool firing;


    private PoseState lastPoseState;
    private MovementState lastMovementState;

    private IControllFireArm firearmController;
    private bool detectAimingChange;

    private void Awake()
    {
        lastMovementState = currentMovementState;
        lastPoseState = currentPoseState;
        detectAimingChange = aiming;
        if(currentWeapon != null) firearmController = currentWeapon.GetComponent<IControllFireArm>();
    }

    private void Update()
    {
        if (lastPoseState != currentPoseState) 
        {
            lastPoseState = currentPoseState;
            OnPoseStateChange();
        }
        if (lastMovementState != currentMovementState) 
        {
            lastMovementState = currentMovementState;
            OnMovementStateChange();
        }
        if (firing) 
        {
            FireWeapon();
        }


        if (aiming && aiming != detectAimingChange)
        {
            detectAimingChange = aiming;

            firearmController.StartAdsWeapon();
        }
        else if(aiming != detectAimingChange) 
        {
            detectAimingChange = aiming;

            firearmController.StopAdsWeapon();
        }
    }

    private void OnPoseStateChange() 
    {
        switch (currentPoseState) 
        {
            case PoseState.Standing:
                break;
            case PoseState.Crouching:
                break;
            case PoseState.Prone:
                break;
        }
    }
    private void OnMovementStateChange() 
    {
        switch (currentMovementState) 
        {
            case MovementState.Still:
                break;
            case MovementState.Walking:
                break;
            case MovementState.Sprinting:
                break;

        }
    }

    public void FireWeapon() 
    {
        firearmController.FireWeapon();
    }

    public void RealodWeapon() 
    {
        firearmController.ReloadWeapon();
    }

    public void NextWeaponAds() 
    {
        firearmController.NextAdsStateWeapon();
    }
}
