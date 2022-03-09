using UnityEngine;
using MyDatatypes.Loadout;

public class SecondaryDropdown_Handler : Custom_MenuDropdown
{
    
    [SerializeField] private SecondaryFirearmType _weaponType;
    protected override void Awake()
    {
        base.Awake();
        GenerateSecondaryDropdownButtons();
    }

    private void GenerateSecondaryDropdownButtons()
    {
        switch (_weaponType)
        {
            case SecondaryFirearmType.Pistols:
                GenerateButtons(LoadoutResources_Handler.Singleton.pistols);
                break;
            case SecondaryFirearmType.Machine_Pistols:
                GenerateButtons(LoadoutResources_Handler.Singleton.machinePistols);
                break;
            case SecondaryFirearmType.Revolvers:
                GenerateButtons(LoadoutResources_Handler.Singleton.revolvers);
                break;
            case SecondaryFirearmType.Others:
                GenerateButtons(LoadoutResources_Handler.Singleton.othersSecondary);
                break;
        }
    }
}
