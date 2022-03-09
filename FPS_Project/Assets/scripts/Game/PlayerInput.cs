using MyDatatypes.GameManager;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private GameState gameState = GameState.Playing;

    #region IN_GAME_INPUT
    public static float look_DeltaX => Input.GetAxis("Mouse X");
    public static float look_DeltaY => Input.GetAxis("Mouse Y");

    #region MOVEMENT_INPUT
    public static float horizontalInput => Input.GetAxis("Horizontal");
    public static float verticalInput => Input.GetAxis("Vertical");

    public static bool Jump_Input => Input.GetKeyDown(KeyCode.Space);

    #endregion

    #region WEAPON INPUT

    public static bool FireAutomatic => Input.GetMouseButton(0);
    public static bool FireSemi => Input.GetMouseButtonDown(0);
    public static bool StartAds => Input.GetMouseButtonDown(1);
    public static bool StopAds => Input.GetMouseButtonUp(1);
    public static bool NextAdsState => Input.GetKeyDown(KeyCode.T);
    public static bool Reload => Input.GetKeyDown(KeyCode.R);

    #endregion

    #region LOADOUT INPUT
    public static bool EquipPrimaryInput => Input.GetKeyDown(KeyCode.Alpha1);
    public static bool EquipSecondaryInput => Input.GetKeyDown(KeyCode.Alpha2);
    public static bool EquipMeleeInput => Input.GetKeyDown(KeyCode.Alpha3);
    #endregion

    #endregion

    public bool hold2Sprint = true;

    [Header("Setup")]
    [SerializeField] private FirstPersonController _fpc;

    private void Update()
    {
        if (gameState == GameState.Playing) 
        {
            HandleMovementInput();
            HandleLoadoutInput();
        }

        
    }

    private static void HandleLoadoutInput()
    {
        if (EquipPrimaryInput) InGameLoadout_Handler.Singleton.EquipPrimary();
        if (EquipSecondaryInput) InGameLoadout_Handler.Singleton.EquipSecondary();
        if (EquipMeleeInput) InGameLoadout_Handler.Singleton.EquipMelee();

        if (InGameLoadout_Handler.Singleton.currentWeaponType != MyDatatypes.Loadout.WeaponType.Melee) 
        {
            if (InGameLoadout_Handler.Singleton.currentFirearmController != null) 
            {
                if (FireAutomatic || FireSemi) { 
                    InGameLoadout_Handler.Singleton.currentFirearmController.FireWeapon();
                }
                if (Reload) { 
                    InGameLoadout_Handler.Singleton.currentFirearmController.ReloadWeapon();
                }
                if (StartAds) { 
                    InGameLoadout_Handler.Singleton.currentFirearmController.StartAdsWeapon();
                }
                if (StopAds) {
                    InGameLoadout_Handler.Singleton.currentFirearmController.StopAdsWeapon();
                }
                if (NextAdsState) { 
                    InGameLoadout_Handler.Singleton.currentFirearmController.NextAdsStateWeapon();
                }
            }
        }
    }

    private void HandleMovementInput()
    {
        _fpc.MovementInput(new Vector2(horizontalInput, verticalInput));
        _fpc.LookInput(new Vector2(look_DeltaX, look_DeltaY));

        if (Jump_Input) _fpc.Jump();

        #region SPRINTING
        if (hold2Sprint)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _fpc.StartSprint();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _fpc.StopSprint();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (_fpc.isSprinting == false)
                {
                    _fpc.StartSprint();
                }
                else
                {
                    _fpc.StopSprint();
                }
            }
        }
        #endregion


        if (!_fpc.isSprinting && Input.GetKeyDown(KeyCode.C))
        {
            _fpc.SetCurrentStateTowardsProning();
        }

        if (!_fpc.isSprinting && Input.GetKeyDown(KeyCode.X))
        {
            _fpc.SetCurrentStateTowardStanding();
        }

        //Slide
        if (_fpc.isSprinting && Input.GetKeyDown(KeyCode.C))
        {
            _fpc.Slide();
        }

        //Dolphine Dive
        if (_fpc.isSprinting && Input.GetKeyDown(KeyCode.X))
        {
            _fpc.DolphinDive();
        }
    }
}
