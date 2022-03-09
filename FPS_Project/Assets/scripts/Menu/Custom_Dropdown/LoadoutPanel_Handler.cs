using MyDatatypes.Loadout;
using UnityEngine;

public class LoadoutPanel_Handler : MonoBehaviour
{
    [Header("Dropdowns")]
    [SerializeField] private GameObject _assaultTypes_Dropdown;
    [SerializeField] private GameObject _scoutTypes_Dropdown;
    [SerializeField] private GameObject _supportTypes_Dropdown;
    [SerializeField] private GameObject _reconTypes_Dropdown;
    [SerializeField] private GameObject _secondaryTypes_Dropdown;
    [SerializeField] private GameObject _meleeTypes_Dropdown;

    
    private GameObject _currentDropdowns;

    private void Awake()
    {
        _currentDropdowns = _assaultTypes_Dropdown;
    }

    public void OnAssaultClassSelected() 
    {
        if (UserData.Singleton.currentClass == LoadoutClass.Assault) return;

        UserData.Singleton.currentClass = LoadoutClass.Assault;
        Menu_Weapon.Singleton.OnClassChanged();

        if(Menu_Weapon.Singleton.currentWeaponType == WeaponType.Primary)
            ChangeDropdowns(_currentDropdowns, _assaultTypes_Dropdown);
    }

    public void OnScoutClassSelected()
    {
        if (UserData.Singleton.currentClass == LoadoutClass.Scout) return;

        UserData.Singleton.currentClass = LoadoutClass.Scout;
        Menu_Weapon.Singleton.OnClassChanged();

        if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Primary)
            ChangeDropdowns(_currentDropdowns, _scoutTypes_Dropdown);
    }

    public void OnSupportClassSelected()
    {
        if (UserData.Singleton.currentClass == LoadoutClass.Support) return;

        UserData.Singleton.currentClass = LoadoutClass.Support;
        Menu_Weapon.Singleton.OnClassChanged();

        if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Primary)
            ChangeDropdowns(_currentDropdowns, _supportTypes_Dropdown);
    }

    public void OnReconClassSelected()
    {
        if (UserData.Singleton.currentClass == LoadoutClass.Reacon) return;

        UserData.Singleton.currentClass = LoadoutClass.Reacon;
        Menu_Weapon.Singleton.OnClassChanged();

        if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Primary)
            ChangeDropdowns(_currentDropdowns, _reconTypes_Dropdown);
    }

    public void OnPrimarySelected()
    {
        if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Primary) return;

        Menu_Weapon.Singleton.currentWeaponType = WeaponType.Primary;

        GameObject _dropdownToDisplayed;
        switch (UserData.Singleton.currentClass) 
        {
            case LoadoutClass.Assault:
                _dropdownToDisplayed = _assaultTypes_Dropdown;
                break;
            case LoadoutClass.Scout:
                _dropdownToDisplayed = _scoutTypes_Dropdown;
                break;
            case LoadoutClass.Support:
                _dropdownToDisplayed = _supportTypes_Dropdown;
                break;
            default:
                _dropdownToDisplayed = _reconTypes_Dropdown;
                break;
        }

        ChangeDropdowns(_currentDropdowns, _dropdownToDisplayed);
    }

    public void OnSecondarySelected()
    {
        if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Secondary) return;

        Menu_Weapon.Singleton.currentWeaponType = WeaponType.Secondary;

        ChangeDropdowns(_currentDropdowns, _secondaryTypes_Dropdown);
    }

    public void OnMeleeSelected()
    {
        if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Melee) return;

        Menu_Weapon.Singleton.currentWeaponType = WeaponType.Melee;

        ChangeDropdowns(_currentDropdowns, _meleeTypes_Dropdown);
    }

    private void ChangeDropdowns(GameObject dropdownToDisable, GameObject dropdownToEnable) 
    {
        dropdownToDisable.SetActive(false);
        dropdownToEnable.SetActive(true);
        _currentDropdowns = dropdownToEnable;
    }
}

