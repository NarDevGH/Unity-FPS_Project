using MyDatatypes.Loadout;
using UnityEngine;

public class PrimaryDropdown_Handler : LoadoutResources_Dropdown
{
    
    [SerializeField] private PrimaryFirearmType _weaponType;

    protected override void Awake()
    {
        base.Awake();
        GeneratePrimaryDropdownButtons();
    }

    private void GeneratePrimaryDropdownButtons()
    {
        switch (_weaponType)
        {
            case PrimaryFirearmType.Assault_Rifle:
                GenerateItems(LoadoutResources_Handler.Singleton.assaultRifles);
                break;
            case PrimaryFirearmType.Battle_Rifle:
                GenerateItems(LoadoutResources_Handler.Singleton.battleRifles);
                break;
            case PrimaryFirearmType.Carbine:
                GenerateItems(LoadoutResources_Handler.Singleton.carbines);
                break;
            case PrimaryFirearmType.Shotgun:
                GenerateItems(LoadoutResources_Handler.Singleton.shotguns);
                break;
            case PrimaryFirearmType.PDW:
                GenerateItems(LoadoutResources_Handler.Singleton.pdws);
                break;
            case PrimaryFirearmType.DMR:
                GenerateItems(LoadoutResources_Handler.Singleton.dmrs);
                break;
            case PrimaryFirearmType.LMG:
                GenerateItems(LoadoutResources_Handler.Singleton.lmgs);
                break;
            case PrimaryFirearmType.Sniper_Rifle:
                GenerateItems(LoadoutResources_Handler.Singleton.sniperRifles);
                break;
        }
    }
}
