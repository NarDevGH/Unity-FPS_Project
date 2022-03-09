using MyDatatypes.Loadout;
using UnityEngine;

public class MeleeDropdown_Handler : Custom_MenuDropdown
{
    
    [SerializeField] private MeleeType _weaponType;
    protected override void Awake()
    {
        base.Awake();
        GenerateMeleeDropdownButtons();
    }

    private void GenerateMeleeDropdownButtons()
    {
        switch (_weaponType)
        {
            case MeleeType.OneHandedBlade:
                GenerateButtons(LoadoutResources_Handler.Singleton.oneHandedBlade);
                break;
            case MeleeType.TwoHandedBlade:
                GenerateButtons(LoadoutResources_Handler.Singleton.twoHandedBlade);
                break;
            case MeleeType.OneHandedBlunt:
                GenerateButtons(LoadoutResources_Handler.Singleton.oneHandedBlunt);
                break;
            case MeleeType.TwoHandedBlunt:
                GenerateButtons(LoadoutResources_Handler.Singleton.twoHandedBlunt);
                break;
        }
    }
}
