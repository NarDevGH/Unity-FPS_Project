using UnityEngine;
using MyDatatypes.Loadout;

public class AttachmentDropdown_Handler : LoadoutResources_Dropdown
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
                GenerateItems(LoadoutResources_Handler.Singleton.optics);
                break;
            case AttachmentType.Barrel:
                GenerateItems(LoadoutResources_Handler.Singleton.barrels);
                break;
            case AttachmentType.Underbarrel:
                GenerateItems(LoadoutResources_Handler.Singleton.underbarrels);
                break;
            case AttachmentType.Other:
                GenerateItems(LoadoutResources_Handler.Singleton.othersAttachments);
                break;
        }
    }

}
