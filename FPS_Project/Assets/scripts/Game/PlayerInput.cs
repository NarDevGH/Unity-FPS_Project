using MyDatatypes.GameManager;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private GameState gameState = GameState.Playing;

    #region IN_GAME_INPUT
    public static float LookDelta_X => Input.GetAxis("Mouse X");
    public static float LookDelta_Y => Input.GetAxis("Mouse Y");

    #region MOVEMENT_INPUT
    public static float Horizontal_Input => Input.GetAxis("Horizontal");
    public static float Vertical_Input => Input.GetAxis("Vertical");

    public static bool Jump_Input => Input.GetKeyDown(KeyCode.Space);

    #endregion

    #region WEAPON INPUT

    public static bool FireInput_Down => Input.GetMouseButtonDown(0);
    public static bool FireInput_Up => Input.GetMouseButtonUp(0);
    public static bool AdsInput_Down => Input.GetMouseButtonDown(1);
    public static bool AdsInput_Up => Input.GetMouseButtonUp(1);
    public static bool NextAdsState_Input => Input.GetKeyDown(KeyCode.T);
    public static bool Reload_Input => Input.GetKeyDown(KeyCode.R);

    #endregion

    #region LOADOUT INPUT
    public static bool EquipPrimary_Input => Input.GetKeyDown(KeyCode.Alpha1);
    public static bool EquipSecondary_Input => Input.GetKeyDown(KeyCode.Alpha2);
    public static bool EquipMelee_Input => Input.GetKeyDown(KeyCode.Alpha3);
    #endregion

    #endregion

    public bool hold2Sprint = true;

    [Header("Setup")]
    [SerializeField] private FirstPersonController _fpc;

    public static PlayerInput singleton { get; private set; }

    private void Awake()
    {
        if (singleton == null) 
        {
            singleton = this;
        }
        else 
        {
            Destroy(this);
        }
    }

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
        if (EquipPrimary_Input) InGameLoadout_Handler.Singleton.EquipPrimary();
        if (EquipSecondary_Input) InGameLoadout_Handler.Singleton.EquipSecondary();
        if (EquipMelee_Input) InGameLoadout_Handler.Singleton.EquipMelee();

        if (InGameLoadout_Handler.Singleton.currentWeaponType != MyDatatypes.Loadout.WeaponType.Melee) 
        {
            if (InGameLoadout_Handler.Singleton.currentFirearmController != null) 
            {
                if (FireInput_Down) { 
                    InGameLoadout_Handler.Singleton.currentFirearmController.StartFireWeapon();
                }
                if (FireInput_Up)
                {
                    InGameLoadout_Handler.Singleton.currentFirearmController.StopFireWeapon();
                }
                if (Reload_Input) { 
                    InGameLoadout_Handler.Singleton.currentFirearmController.ReloadWeapon();
                }
                if (AdsInput_Down) { 
                    InGameLoadout_Handler.Singleton.currentFirearmController.StartAdsWeapon();
                }
                if (AdsInput_Up) {
                    InGameLoadout_Handler.Singleton.currentFirearmController.StopAdsWeapon();
                }
                if (NextAdsState_Input) { 
                    InGameLoadout_Handler.Singleton.currentFirearmController.NextAdsStateWeapon();
                }
            }
        }
    }

    private void HandleMovementInput()
    {
        _fpc.MovementInput(new Vector2(Horizontal_Input, Vertical_Input));
        _fpc.LookInput(new Vector2(LookDelta_X, LookDelta_Y));

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
