using UnityEngine;
using UnityEngine.UI;

public class WeaponDropdownItem : MonoBehaviour, ICustom_MenuDropdown_Item
{
    [SerializeField] private Text _weaponNameText;
    [SerializeField] private GameObject _selectedCheck;


    private object _weaponObj;

    public void InitDropdownItem(string name, Object resourceObj)
    {
        _weaponNameText.text = name;

        _weaponObj = resourceObj;
    }

    public void OnButtonClick() 
    {
        _selectedCheck.SetActive(true);
        Menu_Weapon.Singleton.SetCurrentMenuWeapon(_weaponObj as GameObject);
    }

    
}
