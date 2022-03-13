using MyDatatypes.Loadout;
using UnityEngine;
using UnityEngine.UI;

public class Attachment_DropdownItem : MonoBehaviour, IDropdownItem
{
    [SerializeField] private Text _nameText;

    private object _obj;
    private AttachmentType _attachmentType;

    private LoadoutResources_Handler _loadoutResources => LoadoutResources_Handler.Singleton;

    public void InitDropdownItem(string name, Object resourceObj)
    {
        _nameText.text = name;
        _obj = resourceObj;

        GetAttachmentType(name);
    }

    public void OnButtonClick()
    {
        if (HasPosForAttachment() == false) return;

        AddAttachmentToCurrentWeapon();
    }

    private void AddAttachmentToCurrentWeapon()
    {
        switch (_attachmentType)
        {
            case AttachmentType.Optic:
                FirearmGenerator_Handler.singleton.AddOptic(_obj as GameObject);
                break;
            case AttachmentType.Barrel:
                FirearmGenerator_Handler.singleton.AddBarrel(_obj as GameObject);
                break;
            case AttachmentType.Underbarrel:
                FirearmGenerator_Handler.singleton.AddUnderbarrel(_obj as GameObject);
                break;
            case AttachmentType.Other:
                break;
        }
    }

    private bool HasPosForAttachment()
    {
        bool hasPossforAttachment = false;

            switch (_attachmentType)
            {
                case AttachmentType.Optic:
                    if (FirearmGenerator_Handler.singleton.currentFirearm.attachmentsPos.optic.hasPos) hasPossforAttachment = true;
                    break;
                case AttachmentType.Barrel:
                    if (FirearmGenerator_Handler.singleton.currentFirearm.attachmentsPos.barrel.hasPos) hasPossforAttachment = true;
                    break;
                case AttachmentType.Underbarrel:
                    if (FirearmGenerator_Handler.singleton.currentFirearm.attachmentsPos.underbarrel.hasPos) hasPossforAttachment = true;
                    break;
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
        else if (_loadoutResources.othersAttachments != null && _loadoutResources.othersAttachments.ContainsKey(attachmentName))
        {
            _attachmentType = AttachmentType.Other;
        }

    }
}
