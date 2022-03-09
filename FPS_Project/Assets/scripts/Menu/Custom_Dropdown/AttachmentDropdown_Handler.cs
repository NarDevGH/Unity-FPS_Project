using UnityEngine;
using MyDatatypes.Loadout;

public class AttachmentDropdown_Handler : Custom_MenuDropdown
{
    
    [SerializeField] private AttachmentType _attachmentType;

    protected override void Awake()
    {
        base.Awake();
        GenerateAttachmentDropdownButtons();
    }

    private void GenerateAttachmentDropdownButtons() 
    {
        switch (_attachmentType)
        {
            case AttachmentType.Optic:
                GenerateButtons(LoadoutResources_Handler.Singleton.optics);
                break;
            case AttachmentType.Barrel:
                GenerateButtons(LoadoutResources_Handler.Singleton.barrels);
                break;
            case AttachmentType.Underbarrel:
                GenerateButtons(LoadoutResources_Handler.Singleton.underbarrels);
                break;
            case AttachmentType.Other:
                GenerateButtons(LoadoutResources_Handler.Singleton.othersAttachments);
                break;
        }
    }

    protected override void OnClick_Dropdown()
    {
        bool weaponHasAttachmentPos = false;

        if (Menu_Weapon.Singleton.currentWeaponType == WeaponType.Primary)
        {
            switch (_attachmentType)
            {
                case AttachmentType.Optic:
                    weaponHasAttachmentPos = Menu_Weapon.Singleton.currentMenuLoadout.primary.attachments.optic.hasPos;
                    break;
                case AttachmentType.Barrel:
                    weaponHasAttachmentPos = Menu_Weapon.Singleton.currentMenuLoadout.primary.attachments.barrel.hasPos;
                    break;
                case AttachmentType.Underbarrel:
                    weaponHasAttachmentPos = Menu_Weapon.Singleton.currentMenuLoadout.primary.attachments.underbarrel.hasPos;
                    break;
            }
        }
        else 
        {
            switch (_attachmentType)
            {
                case AttachmentType.Optic:
                    weaponHasAttachmentPos = Menu_Weapon.Singleton.currentMenuLoadout.secondary.attachments.optic.hasPos;
                    break;
                case AttachmentType.Barrel:
                    weaponHasAttachmentPos = Menu_Weapon.Singleton.currentMenuLoadout.secondary.attachments.barrel.hasPos;
                    break;
                case AttachmentType.Underbarrel:
                    weaponHasAttachmentPos = Menu_Weapon.Singleton.currentMenuLoadout.secondary.attachments.underbarrel.hasPos;
                    break;
            }
        }

        if (weaponHasAttachmentPos) 
        {
            base.OnClick_Dropdown();
        }
    }

}
