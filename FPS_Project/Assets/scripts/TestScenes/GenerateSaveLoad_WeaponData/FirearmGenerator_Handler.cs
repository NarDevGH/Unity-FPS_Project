using UnityEngine;
using MyDatatypes.Firearm;

public class FirearmGenerator_Handler : MonoBehaviour
{

    public static FirearmGenerator_Handler singleton { get; set; }

    [SerializeField] private Transform _firearmPos;

    public FirearmData currentFirearm;


    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    public void SetCurrentWeapon(GameObject firearm) 
    {
        if (currentFirearm.name == firearm.name) return;

        if (currentFirearm.firearm) Destroy(currentFirearm.firearm);

        currentFirearm.firearm = Instantiate(firearm, _firearmPos);
        currentFirearm.firearm.name = currentFirearm.firearm.name.Replace("(Clone)", "");
        currentFirearm.name = currentFirearm.firearm.name;

        currentFirearm.attachmentsPos = GetAttachmentsPosData(currentFirearm.firearm);
    }

    public void AddOptic(GameObject optic) 
    {
        if (currentFirearm.attachments.optic.attachment != null)
        {
            //on selected again the same attachment, remove it
            if (currentFirearm.attachments.optic.attachment.name == optic.name)
            {
                Destroy(currentFirearm.attachmentsPos.optic.pos.GetChild(0).gameObject);
                currentFirearm.attachments.optic.attachment = null;
                currentFirearm.attachments.optic.name = null;
                //UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.optic.name = null;
                return;
            }
            else //Destroy old Optic
            {
                Destroy(currentFirearm.attachmentsPos.optic.pos.GetChild(0).gameObject);
            }
        }

            currentFirearm.attachments.optic.attachment = Instantiate(optic, currentFirearm.attachmentsPos.optic.pos);
            currentFirearm.attachments.optic.attachment.name = currentFirearm.attachments.optic.attachment.name.Replace("(Clone)", "");
            currentFirearm.attachments.optic.name = currentFirearm.attachments.optic.attachment.name;
            //UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.optic.name = currentFirearm.attachments.optic.attachment.name;
    }

    public void AddBarrel(GameObject barrel)
    {
        if (currentFirearm.attachments.barrel.attachment != null)
        {
            //on selected again the same attachment, remove it
            if (currentFirearm.attachments.barrel.attachment.name == barrel.name)
            {
                Destroy(currentFirearm.attachmentsPos.optic.pos.GetChild(0).gameObject);
                currentFirearm.attachments.barrel.attachment = null;
                currentFirearm.attachments.barrel.name = null;
                //UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.barrel.name = null;
                return;
            }
            else //Destroy old Optic
            {
                Destroy(currentFirearm.attachmentsPos.barrel.pos.GetChild(0).gameObject);
            }
        }

            currentFirearm.attachments.barrel.attachment = Instantiate(barrel, currentFirearm.attachmentsPos.barrel.pos);
            currentFirearm.attachments.barrel.attachment.name = currentFirearm.attachments.barrel.attachment.name.Replace("(Clone)", "");
            currentFirearm.attachments.barrel.name = currentFirearm.attachments.barrel.attachment.name;

            //UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.barrel.name = currentFirearm.attachments.barrel.attachment.name;
    }

    public void AddUnderbarrel(GameObject underbarrel)
    {
        if (currentFirearm.attachments.underbarrel.attachment != null)
        {
            //on selected again the same attachment, remove it
            if (currentFirearm.attachments.underbarrel.attachment.name == underbarrel.name)
            {
                Destroy(currentFirearm.attachmentsPos.underbarrel.pos.GetChild(0).gameObject);
                currentFirearm.attachments.underbarrel.attachment = null;
                currentFirearm.attachments.underbarrel.name = null;
                //UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.underbarrel.name = null;
                return;
            }
            else //Destroy old Optic
            {
                Destroy(currentFirearm.attachmentsPos.underbarrel.pos.GetChild(0).gameObject);
            }
        }

            currentFirearm.attachments.underbarrel.attachment = Instantiate(underbarrel, currentFirearm.attachmentsPos.barrel.pos);
            currentFirearm.attachments.underbarrel.attachment.name = currentFirearm.attachments.underbarrel.attachment.name.Replace("(Clone)", "");
            currentFirearm.attachments.underbarrel.name = currentFirearm.attachments.underbarrel.attachment.name;

            //UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.underbarrel.name = currentFirearm.attachments.underbarrel.attachment.name;
    }

    public void GenerateWeapon(FirearmData firearmData) 
    {
        SetCurrentWeapon(LoadoutResources_Handler.Singleton.MenuPrimaryGameObject(firearmData.name));
        if (firearmData.attachments.optic.name != null && firearmData.attachments.optic.name != "") {
            AddOptic(LoadoutResources_Handler.Singleton.optics[firearmData.attachments.optic.name].menuPrefab as GameObject);
        }
        if (firearmData.attachments.barrel.name != null && firearmData.attachments.barrel.name != "") { 
            AddBarrel(LoadoutResources_Handler.Singleton.barrels[firearmData.attachments.barrel.name].menuPrefab as GameObject);
        }
        if (firearmData.attachments.underbarrel.name != null && firearmData.attachments.underbarrel.name != "") 
        {
            AddUnderbarrel(LoadoutResources_Handler.Singleton.underbarrels[firearmData.attachments.underbarrel.name].menuPrefab as GameObject);
        }
    }

    private AttachmentsPosData GetAttachmentsPosData(GameObject firearm)
    {
        AttachmentsPosData attachmentsData = new AttachmentsPosData();

        Transform opticPos = firearm.transform.Find("Optic_Pos");
        attachmentsData.optic.pos = opticPos == null ? null : opticPos;
        attachmentsData.optic.hasPos = opticPos != null;

        Transform barrelPos = firearm.transform.Find("Barrel_Pos");
        attachmentsData.barrel.pos = barrelPos == null ? null : barrelPos;
        attachmentsData.barrel.hasPos = barrelPos != null;

        Transform underbarrelPos = firearm.transform.Find("Underbarrel_Pos");
        attachmentsData.underbarrel.pos = underbarrelPos == null ? null : underbarrelPos;
        attachmentsData.underbarrel.hasPos = underbarrelPos != null;

        return attachmentsData;
    }
}
