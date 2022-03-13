using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firearm_DropdownItem : MonoBehaviour, IDropdownItem
{
    [SerializeField] private Text _weaponNameText;
    private object _weaponObj;
    public void InitDropdownItem(string name, Object resourceObj)
    {
        _weaponNameText.text = name;

        _weaponObj = resourceObj;
    }

    public void OnButtonClick()
    {
        if (FirearmGenerator_Handler.singleton) FirearmGenerator_Handler.singleton.SetCurrentWeapon(_weaponObj as GameObject);
    }
}
