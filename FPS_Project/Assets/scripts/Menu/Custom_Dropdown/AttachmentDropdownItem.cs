using UnityEngine;
using UnityEngine.UI;
using MyDatatypes.Loadout;

public class AttachmentDropdownItem : MonoBehaviour,ICustom_MenuDropdown_Item
{
    [SerializeField] private Text attachmentNameText;
    [SerializeField] private GameObject _selectedCheck;

    private Object _attachmentObj;
    private AttachmentType _attachmentType;

    private LoadoutResources_Handler _loadoutResources => LoadoutResources_Handler.Singleton;


    public void InitDropdownItem(string name, Object resourceObj)
    {
        attachmentNameText.text = name;

        GetAttachmentType(name);

        _attachmentObj = resourceObj;
    }

    

    public void OnButtonClick()
    {
        if (HasPosForAttachment() == false) return;

        AddAttachmentToMenuWeapon();
    }

    private void AddAttachmentToMenuWeapon()
    {
        switch (_attachmentType)
        {
            case AttachmentType.Optic:
                Menu_Weapon.Singleton.AddOptic(_attachmentObj as GameObject);
                break;
            case AttachmentType.Barrel:
                Menu_Weapon.Singleton.AddBarrel(_attachmentObj as GameObject);
                break;
            case AttachmentType.Underbarrel:
                Menu_Weapon.Singleton.AddUnderbarrel(_attachmentObj as GameObject);
                break;
            case AttachmentType.Other:
                break;
        }
    }

    private bool HasPosForAttachment()
    {
        bool hasPossforAttachment = false;

        if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Primary)
        {
            switch (_attachmentType)
            {
                case AttachmentType.Optic:
                    if (Menu_Weapon.Singleton.currentMenuLoadout.primary.attachments.optic.hasPos) hasPossforAttachment = true;
                    break;
                case AttachmentType.Barrel:
                    if (Menu_Weapon.Singleton.currentMenuLoadout.primary.attachments.barrel.hasPos) hasPossforAttachment = true;
                    break;
                case AttachmentType.Underbarrel:
                    if (Menu_Weapon.Singleton.currentMenuLoadout.primary.attachments.underbarrel.hasPos) hasPossforAttachment = true;
                    break;
            }
        }
        else if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Secondary)
        {
            switch (_attachmentType)
            {
                case AttachmentType.Optic:
                    if (Menu_Weapon.Singleton.currentMenuLoadout.secondary.attachments.optic.hasPos) hasPossforAttachment = true;
                    break;
                case AttachmentType.Barrel:
                    if (Menu_Weapon.Singleton.currentMenuLoadout.secondary.attachments.barrel.hasPos) hasPossforAttachment = true;
                    break;
                case AttachmentType.Underbarrel:
                    if (Menu_Weapon.Singleton.currentMenuLoadout.secondary.attachments.underbarrel.hasPos) hasPossforAttachment = true;
                    break;
            }
        }

        return hasPossforAttachment;
    }

    private void GetAttachmentType(string attachmentName)
    {
        if (_loadoutResources.optics != null && _loadoutResources.optics.ContainsKey(attachmentName))
        {
            _attachmentType = AttachmentType.Optic;
        }
        else if (_loadoutResources.barrels != null && _loadoutResources.barrels.ContainsKey(attachmentName))
        {
            _attachmentType = AttachmentType.Barrel;
        }
        else if (_loadoutResources.underbarrels != null && _loadoutResources.underbarrels.ContainsKey(attachmentName))
        {
            _attachmentType = AttachmentType.Underbarrel;
        }
        else if(_loadoutResources.othersAttachments != null && _loadoutResources.othersAttachments.ContainsKey(attachmentName))
        {
            _attachmentType = AttachmentType.Other;
        }

    }
}
