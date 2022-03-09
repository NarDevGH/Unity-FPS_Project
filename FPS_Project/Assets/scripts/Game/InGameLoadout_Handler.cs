using MyDatatypes.Loadout;
using UnityEngine;

public class InGameLoadout_Handler : MonoBehaviour
{
    [SerializeField] private Transform _primaryParent;
    [SerializeField] private Transform _secondaryParent;
    [SerializeField] private Transform _meleeParents;

    private IControllFireArm _primaryFirearmController;
    private IControllFireArm _secondaryFirearmController;

    public Transform currentWeaponParent { get; private set; }
    public WeaponType currentWeaponType { get; private set; }
    public IControllFireArm currentFirearmController { get; private set; }

    public static InGameLoadout_Handler Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    public void GenerateInGameLoadout(GameObject primary = null, GameObject secondary = null, GameObject melee = null, WeaponType startType = WeaponType.Primary) 
    {
        GameObject inGamePrimary = null, inGameSecondary = null, inGameMelee = null;

        if (primary) 
        {
            inGamePrimary = Instantiate(primary, _primaryParent);
            inGamePrimary.name = inGamePrimary.name.Replace("(Clone)", "");
            _primaryFirearmController = inGamePrimary.GetComponent<IControllFireArm>();
            AddPrimaryAttachments(inGamePrimary);
            Firearm_Ads adsHandler = inGamePrimary.GetComponent<Firearm_Ads>();
            if(adsHandler) adsHandler.InitAdsHandler();
        }

        if (secondary) 
        {
            inGameSecondary = Instantiate(secondary, _secondaryParent);
            inGameSecondary.name = inGameSecondary.name.Replace("(Clone)", "");
            _secondaryFirearmController = inGameSecondary.GetComponent<IControllFireArm>();
            AddSecondaryAttachments(inGameSecondary);
            Firearm_Ads adsHandler = inGameSecondary.GetComponent<Firearm_Ads>();
            if (adsHandler) adsHandler.InitAdsHandler();
        }

        if (melee) 
        {
            inGameMelee = Instantiate(melee, _meleeParents);
            inGameMelee.name = inGameMelee.name.Replace("(Clone)", "");
            //Get MeleeController
        }

        switch (startType) 
        {
            case WeaponType.Primary:
                currentWeaponType = WeaponType.Primary;
                currentWeaponParent = _primaryParent;
                currentFirearmController = _primaryFirearmController;
                break;
            case WeaponType.Secondary:
                currentWeaponType = WeaponType.Secondary;
                currentWeaponParent = _secondaryParent;
                currentFirearmController = _secondaryFirearmController;
                break;
            case WeaponType.Melee:
                currentWeaponType = WeaponType.Melee;
                currentWeaponParent = _meleeParents;
                break;

        }
        currentWeaponParent.gameObject.SetActive(true);
    }

    
    private void AddPrimaryAttachments(GameObject primary)
    {
        Transform opticPos = primary.transform.Find("Attachments_Pos/Optic_Pos");
        string opticName = UserData.Singleton.currentClassLoadout.primaryData.attachments.optic.name;
        if (opticPos != null && opticName != null) 
        {
            GameObject optic = Instantiate(LoadoutResources_Handler.Singleton.optics[opticName].inGamePrefab as GameObject,opticPos);
            optic.name = optic.name.Replace("(Clone)", "");

            Firearm_Ads adsHandler = primary.GetComponent<Firearm_Ads>();
            Optic_Attachment opticAttachment = optic.GetComponent<Optic_Attachment>();
            if (adsHandler && opticAttachment) 
                adsHandler.opticAdsStates = opticAttachment.opticAdsStates;
        }

        Transform barrelPos = primary.transform.Find("Attachments_Pos/Barrel_Pos");
        string barrelName = UserData.Singleton.currentClassLoadout.primaryData.attachments.barrel.name;
        if (barrelPos != null && barrelName != null)
        {
            GameObject barrel = Instantiate( LoadoutResources_Handler.Singleton.barrels[barrelName].inGamePrefab as GameObject, barrelPos);
            barrel.name = barrel.name.Replace("(Clone)", "");
        }

        Transform underbarrelPos = primary.transform.Find("Attachments_Pos/Underbarrel_Pos");
        string underbarrelName = UserData.Singleton.currentClassLoadout.primaryData.attachments.underbarrel.name;
        if (underbarrelPos != null && underbarrelName != null)
        {
            GameObject underbarrel = Instantiate( LoadoutResources_Handler.Singleton.underbarrels[underbarrelName].inGamePrefab as GameObject, underbarrelPos);
            underbarrel.name = underbarrel.name.Replace("(Clone)", "");
        }
    }
    private void AddSecondaryAttachments(GameObject secondary)
    {
        Transform opticPos = secondary.transform.Find("Attachments_Pos/Optic_Pos");
        string opticName = UserData.Singleton.currentClassLoadout.secondaryData.attachments.optic.name;
        if (opticPos != null && opticName != null)
        {
            GameObject optic = Instantiate( LoadoutResources_Handler.Singleton.optics[opticName].inGamePrefab as GameObject, opticPos);
            optic.name = optic.name.Replace("(Clone)", "");

            Firearm_Ads adsHandler = secondary.GetComponent<Firearm_Ads>();
            Optic_Attachment opticAttachment = optic.GetComponent<Optic_Attachment>();
            if (adsHandler && opticAttachment)
                adsHandler.opticAdsStates = opticAttachment.opticAdsStates;
        }

        Transform barrelPos = secondary.transform.Find("Attachments_Pos/Barrel_Pos");
        string barrelName = UserData.Singleton.currentClassLoadout.secondaryData.attachments.barrel.name;
        if (barrelPos != null && barrelName != null)
        {
            GameObject barrel = Instantiate( LoadoutResources_Handler.Singleton.barrels[barrelName].inGamePrefab as GameObject, barrelPos);
            barrel.name = barrel.name.Replace("(Clone)", "");
        }

        Transform underbarrelPos = secondary.transform.Find("Attachments_Pos/Underbarrel_Pos");
        string underbarrelName = UserData.Singleton.currentClassLoadout.primaryData.attachments.underbarrel.name;
        if (underbarrelPos != null && underbarrelName != null)
        {
            GameObject underbarrel = Instantiate( LoadoutResources_Handler.Singleton.underbarrels[underbarrelName].inGamePrefab as GameObject, underbarrelPos);
            underbarrel.name = underbarrel.name.Replace("(Clone)", "");
        }
    }

    public void EquipPrimary() 
    {
        if (currentWeaponParent == _primaryParent || _primaryParent == null) return;

        currentWeaponParent.gameObject.SetActive(false);

        currentWeaponParent = _primaryParent;
        currentFirearmController = _primaryFirearmController;

        currentWeaponParent.gameObject.SetActive(true);
    }

    public void EquipSecondary()
    {
        if (currentWeaponParent == _secondaryParent || _secondaryParent == null) return;

        currentWeaponParent.gameObject.SetActive(false);

        currentWeaponParent = _secondaryParent;
        currentFirearmController = _secondaryFirearmController;

        currentWeaponParent.gameObject.SetActive(true);
    }

    public void EquipMelee()
    {
        if (currentWeaponParent == _meleeParents || _meleeParents == null) return;
        currentWeaponParent.gameObject.SetActive(false);
        currentWeaponParent = _meleeParents;
        currentWeaponParent.gameObject.SetActive(true);
    }
}
