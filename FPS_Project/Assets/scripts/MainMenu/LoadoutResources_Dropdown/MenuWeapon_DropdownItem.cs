using UnityEngine;
using UnityEngine.UI;

public class MenuWeapon_DropdownItem : MonoBehaviour, IDropdownItem
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
        if(Menu_Weapon.Singleton) Menu_Weapon.Singleton.SetCurrentMenuWeapon(_weaponObj as GameObject);
    }

    
}
