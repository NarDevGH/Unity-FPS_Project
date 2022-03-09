using MyDatatypes.Loadout;
using UnityEngine;

public class PrimaryDropdown_Handler : Custom_MenuDropdown
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
                GenerateButtons(LoadoutResources_Handler.Singleton.assaultRifles);
                break;
            case PrimaryFirearmType.Battle_Rifle:
                GenerateButtons(LoadoutResources_Handler.Singleton.battleRifles);
                break;
            case PrimaryFirearmType.Carbine:
                GenerateButtons(LoadoutResources_Handler.Singleton.carbines);
                break;
            case PrimaryFirearmType.Shotgun:
                GenerateButtons(LoadoutResources_Handler.Singleton.shotguns);
                break;
            case PrimaryFirearmType.PDW:
                GenerateButtons(LoadoutResources_Handler.Singleton.pdws);
                break;
            case PrimaryFirearmType.DMR:
                GenerateButtons(LoadoutResources_Handler.Singleton.dmrs);
                break;
            case PrimaryFirearmType.LMG:
                GenerateButtons(LoadoutResources_Handler.Singleton.lmgs);
                break;
            case PrimaryFirearmType.Sniper_Rifle:
                GenerateButtons(LoadoutResources_Handler.Singleton.sniperRifles);
                break;
        }
    }
}
