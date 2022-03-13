using UnityEngine;
using MyDatatypes.Loadout;

public class SecondaryDropdown_Handler : LoadoutResources_Dropdown
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
                GenerateItems(LoadoutResources_Handler.Singleton.pistols);
                break;
            case SecondaryFirearmType.Machine_Pistols:
                GenerateItems(LoadoutResources_Handler.Singleton.machinePistols);
                break;
            case SecondaryFirearmType.Revolvers:
                GenerateItems(LoadoutResources_Handler.Singleton.revolvers);
                break;
            case SecondaryFirearmType.Others:
                GenerateItems(LoadoutResources_Handler.Singleton.othersSecondary);
                break;
        }
    }
}
