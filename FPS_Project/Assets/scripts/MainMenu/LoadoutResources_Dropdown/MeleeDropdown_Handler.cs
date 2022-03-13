using MyDatatypes.Loadout;
using UnityEngine;

public class MeleeDropdown_Handler : LoadoutResources_Dropdown
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
                GenerateItems(LoadoutResources_Handler.Singleton.oneHandedBlade);
                break;
            case MeleeType.TwoHandedBlade:
                GenerateItems(LoadoutResources_Handler.Singleton.twoHandedBlade);
                break;
            case MeleeType.OneHandedBlunt:
                GenerateItems(LoadoutResources_Handler.Singleton.oneHandedBlunt);
                break;
            case MeleeType.TwoHandedBlunt:
                GenerateItems(LoadoutResources_Handler.Singleton.twoHandedBlunt);
                break;
        }
    }
}
