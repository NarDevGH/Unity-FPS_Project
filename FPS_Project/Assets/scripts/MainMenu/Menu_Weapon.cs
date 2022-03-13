using UnityEngine;
using MyDatatypes.GameResources;
using MyDatatypes.Loadout;
using MyDatatypes.Firearm;

public class Menu_Weapon : MonoBehaviour
{
    [SerializeField] private Transform _menuWeaponParent;

    private WeaponType _currentWeaponType;
    public WeaponType currentWeaponType { 
        get { return _currentWeaponType; }
        set 
        {
            _currentWeaponType = value;
            OnWeaponTypeChange();
        }
    }

    public LoadoutData currentMenuLoadout;

    private LoadoutData _menuAssaultLoadout;
    private LoadoutData _menuScoutLoadout;
    private LoadoutData _menuSupportLoadout;
    private LoadoutData _menuReaconLoadout;

    public static Menu_Weapon Singleton;

    private void Awake()
    {
        HandleInstance();
        Init();
    }

    private void Init()
    {
        _menuAssaultLoadout = MenuClassLoadoutByUserDataLoadout(UserData.Singleton.assaultLoadout);
        _menuScoutLoadout = MenuClassLoadoutByUserDataLoadout(UserData.Singleton.scoutLoadout);
        _menuSupportLoadout = MenuClassLoadoutByUserDataLoadout(UserData.Singleton.supportLoadout);
        _menuReaconLoadout = MenuClassLoadoutByUserDataLoadout(UserData.Singleton.reaconLoadout);

        switch (UserData.Singleton.currentClass)
        {
            case LoadoutClass.Assault:
                currentMenuLoadout = _menuAssaultLoadout;
                break;
            case LoadoutClass.Scout:
                currentMenuLoadout = _menuScoutLoadout;
                break;
            case LoadoutClass.Support:
                currentMenuLoadout = _menuSupportLoadout;
                break;
            case LoadoutClass.Reacon:
                currentMenuLoadout = _menuReaconLoadout;
                break;
        }

        currentWeaponType = WeaponType.Primary;
    }

    public void OnClassChanged() 
    {
        switch (UserData.Singleton.currentClass) 
        {
            case LoadoutClass.Assault:
                _menuAssaultLoadout = currentMenuLoadout;
                UserData.Singleton.assaultLoadout = UserData.Singleton.currentClassLoadout;
                break;
            case LoadoutClass.Scout:
                _menuScoutLoadout = currentMenuLoadout;
                UserData.Singleton.scoutLoadout = UserData.Singleton.currentClassLoadout;
                break;
            case LoadoutClass.Support:
                _menuSupportLoadout = currentMenuLoadout;
                UserData.Singleton.supportLoadout = UserData.Singleton.currentClassLoadout;
                break;
            case LoadoutClass.Reacon:
                _menuReaconLoadout = currentMenuLoadout;
                UserData.Singleton.reaconLoadout = UserData.Singleton.currentClassLoadout;
                break;
        } //save class loadout

        switch (_currentWeaponType)
        {
            case WeaponType.Primary:
                currentMenuLoadout.primaryData.firearmData.firearm.SetActive(false);
                break;
            case WeaponType.Secondary:
                currentMenuLoadout.secondaryData.firearmData.firearm.SetActive(false);
                break;
            case WeaponType.Melee:
                currentMenuLoadout.meleeData.melee.SetActive(false);
                break;
        } //disable current weaponType

        switch (UserData.Singleton.currentClass) 
        {
            case LoadoutClass.Assault:
                currentMenuLoadout = _menuAssaultLoadout;
                UserData.Singleton.currentClassLoadout = UserData.Singleton.assaultLoadout;
                break;
            case LoadoutClass.Scout:
                currentMenuLoadout = _menuScoutLoadout;
                UserData.Singleton.currentClassLoadout = UserData.Singleton.scoutLoadout;
                break;
            case LoadoutClass.Support:
                currentMenuLoadout = _menuSupportLoadout;
                UserData.Singleton.currentClassLoadout = UserData.Singleton.supportLoadout;
                break;
            case LoadoutClass.Reacon:
                currentMenuLoadout = _menuReaconLoadout;
                UserData.Singleton.currentClassLoadout = UserData.Singleton.reaconLoadout;
                break;
        }  // set current class loadout

        switch (_currentWeaponType) 
        {
            case WeaponType.Primary:
                currentMenuLoadout.primaryData.firearmData.firearm.SetActive(true);
                break;
            case WeaponType.Secondary:
                currentMenuLoadout.secondaryData.firearmData.firearm.SetActive(true);
                break;
            case WeaponType.Melee:
                currentMenuLoadout.meleeData.melee.SetActive(true);
                break;
        } //enable current weaponType
    }

    


    #region SET_CURRENT_MENU_WEAPON_METHODS

    public void SetCurrentMenuWeapon(GameObject weapon) 
    {
        switch (currentWeaponType) 
        {
            case WeaponType.Primary:
                if (currentMenuLoadout.primaryData.firearmData.name == weapon.name) return;

                SetCurrentMenuPrimary(weapon);
                break;

            case WeaponType.Secondary:
                if (currentMenuLoadout.secondaryData.firearmData.name == weapon.name) return;

                SetCurrentMenuSecondary(weapon);
                break;

            case WeaponType.Melee:
                if (currentMenuLoadout.meleeData.name == weapon.name) return;

                SetCurrentMenuMelee(weapon);
                break;
        }
    }

    private void SetCurrentMenuPrimary(GameObject weapon)
    {
        Destroy(currentMenuLoadout.primaryData.firearmData.firearm); // Destroy old weapon

        currentMenuLoadout.primaryData.firearmData.firearm = Instantiate(weapon, _menuWeaponParent);
        currentMenuLoadout.primaryData.firearmData.attachmentsPos= GetAttachmentsPosData(currentMenuLoadout.primaryData.firearmData.firearm);
        currentMenuLoadout.primaryData.firearmData.firearm.name = currentMenuLoadout.primaryData.firearmData.firearm.name.Replace("(Clone)", "");
        currentMenuLoadout.primaryData.firearmData.name = currentMenuLoadout.primaryData.firearmData.firearm.name;

        UserData.Singleton.currentClassLoadout.primaryData = currentMenuLoadout.primaryData;
    }

    private void SetCurrentMenuSecondary(GameObject weapon)
    {
        Destroy(currentMenuLoadout.secondaryData.firearmData.firearm);

        currentMenuLoadout.secondaryData.firearmData.firearm = Instantiate(weapon, _menuWeaponParent);
        currentMenuLoadout.secondaryData.firearmData.attachmentsPos = GetAttachmentsPosData(currentMenuLoadout.secondaryData.firearmData.firearm);
        currentMenuLoadout.secondaryData.firearmData.name = currentMenuLoadout.secondaryData.firearmData.firearm.name.Replace("(Clone)", "");
        currentMenuLoadout.secondaryData.firearmData.name = currentMenuLoadout.secondaryData.firearmData.firearm.name;

        UserData.Singleton.currentClassLoadout.secondaryData = currentMenuLoadout.secondaryData;
    }

    private void SetCurrentMenuMelee(GameObject weapon)
    {
        Destroy(currentMenuLoadout.meleeData.melee);

        currentMenuLoadout.meleeData.melee = Instantiate(weapon, _menuWeaponParent);

        currentMenuLoadout.meleeData.melee.name = currentMenuLoadout.meleeData.melee.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.meleeData.name = currentMenuLoadout.meleeData.melee.name;
        UserData.Singleton.currentClassLoadout.meleeData.type = LoadoutResources_Handler.Singleton.GetMeleeType(currentMenuLoadout.meleeData.melee);
    }

    #endregion


    #region ADD_OPTIC_METHODS

    public void AddOptic(GameObject optic) 
    {
        switch (_currentWeaponType)
        {
            case WeaponType.Primary:
                if (currentMenuLoadout.secondaryData.firearmData.attachmentsPos.optic.hasPos == false) return;

                AddOpticToCurrentLoadoutPrimary(optic);
                break;

            case WeaponType.Secondary:
                if (currentMenuLoadout.secondaryData.firearmData.attachmentsPos.optic.hasPos == false) return;

                AddOpticToCurrentLoadoutSecondary(optic);
                break;
        }
    }

    private void AddOpticToCurrentLoadoutPrimary(GameObject optic)
    {
        if (currentMenuLoadout.primaryData.firearmData.attachments.optic.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.primaryData.firearmData.attachments.optic.attachment.name == optic.name)
            {
                Destroy(currentMenuLoadout.primaryData.firearmData.attachmentsPos.optic.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.optic.name = null;
                currentMenuLoadout.primaryData.firearmData.attachments.optic.attachment = null;
                return;
            }
            else //Destroy old Optic
            {
                Destroy(currentMenuLoadout.primaryData.firearmData.attachmentsPos.optic.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.primaryData.firearmData.attachments.optic.attachment = Instantiate(optic,currentMenuLoadout.primaryData.firearmData.attachmentsPos.optic.pos);

        currentMenuLoadout.primaryData.firearmData.attachments.optic.attachment.name = currentMenuLoadout.primaryData.firearmData.attachments.optic.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.optic.name = currentMenuLoadout.primaryData.firearmData.attachments.optic.attachment.name;
    }

    private void AddOpticToCurrentLoadoutSecondary(GameObject optic)
    {
        
        if (currentMenuLoadout.secondaryData.firearmData.attachments.optic.attachment != null)
        {   //on selected again the same attachment, remove it
            if (currentMenuLoadout.secondaryData.firearmData.attachments.optic.attachment.name == optic.name)
            {
                Destroy(currentMenuLoadout.secondaryData.firearmData.attachmentsPos.optic.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.secondaryData.firearmData.attachments.optic.name = null;
                currentMenuLoadout.secondaryData.firearmData.attachments.optic.attachment = null;
                return;
            }
            else //Destroy old Optic
            {
                Destroy(currentMenuLoadout.secondaryData.firearmData.attachmentsPos.optic.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.secondaryData.firearmData.attachments.optic.attachment = Instantiate(optic,currentMenuLoadout.secondaryData.firearmData.attachmentsPos.optic.pos);

        currentMenuLoadout.secondaryData.firearmData.attachments.optic.attachment.name = currentMenuLoadout.secondaryData.firearmData.attachments.optic.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.secondaryData.firearmData.attachments.optic.name = currentMenuLoadout.secondaryData.firearmData.attachments.optic.name;
    }

    #endregion

    #region ADD_BARREL_METHODS

    public void AddBarrel(GameObject barrel)
    {
        switch (_currentWeaponType)
        {
            case WeaponType.Primary:
                if (currentMenuLoadout.secondaryData.firearmData.attachmentsPos.barrel.hasPos == false) return;

                AddBarrelToCurrentLoadoutPrimary(barrel);
                break;

            case WeaponType.Secondary:
                if (currentMenuLoadout.secondaryData.firearmData.attachmentsPos.barrel.hasPos == false) return;

                AddBarrelToCurrentLoadoutSecondary(barrel);
                break;
        }
    }

    private void AddBarrelToCurrentLoadoutPrimary(GameObject barrel)
    {
        if (currentMenuLoadout.primaryData.firearmData.attachments.barrel.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.primaryData.firearmData.attachments.barrel.attachment.name == barrel.name)
            {
                Destroy(currentMenuLoadout.primaryData.firearmData.attachmentsPos.barrel.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.barrel.name = null;
                currentMenuLoadout.primaryData.firearmData.attachments.barrel.attachment = null;
                return;
            }
            else //Destroy old Berrel 
            {
                Destroy(currentMenuLoadout.primaryData.firearmData.attachmentsPos.barrel.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.primaryData.firearmData.attachments.barrel.attachment = Instantiate(barrel, currentMenuLoadout.primaryData.firearmData.attachmentsPos.barrel.pos);

        currentMenuLoadout.primaryData.firearmData.attachments.barrel.attachment.name = currentMenuLoadout.primaryData.firearmData.attachments.barrel.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.barrel.name = currentMenuLoadout.primaryData.firearmData.attachments.barrel.attachment.name;
    }

    private void AddBarrelToCurrentLoadoutSecondary(GameObject barrel)
    {
        if (currentMenuLoadout.secondaryData.firearmData.attachments.barrel.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.secondaryData.firearmData.attachments.barrel.attachment.name == barrel.name)
            {
                Destroy(currentMenuLoadout.secondaryData.firearmData.attachmentsPos.barrel.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.secondaryData.firearmData.attachments.barrel.name = null;
                currentMenuLoadout.secondaryData.firearmData.attachments.barrel.attachment = null;
                return;
            }
            else 
            {
                Destroy(currentMenuLoadout.secondaryData.firearmData.attachmentsPos.barrel.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.secondaryData.firearmData.attachments.barrel.attachment = Instantiate(barrel, currentMenuLoadout.secondaryData.firearmData.attachmentsPos.barrel.pos);

        currentMenuLoadout.secondaryData.firearmData.attachments.barrel.attachment.name = currentMenuLoadout.secondaryData.firearmData.attachments.barrel.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.secondaryData.firearmData.attachments.barrel.name = currentMenuLoadout.secondaryData.firearmData.attachments.barrel.attachment.name;
    }

    #endregion

    #region ADD_UNDERBARREL_METHODS

    public void AddUnderbarrel(GameObject underbarrel)
    {
        switch (_currentWeaponType)
        {
            case WeaponType.Primary:
                if (currentMenuLoadout.primaryData.firearmData.attachmentsPos.underbarrel.hasPos == false) return;

                AddUnderbarrelToCurrentLoadoutPrimary(underbarrel);
                break;

            case WeaponType.Secondary:
                if (currentMenuLoadout.secondaryData.firearmData.attachmentsPos.underbarrel.hasPos == false) return;

                AddUnderbarrelToCurrentLoadoutSecondary(underbarrel);
                break;
        }
    }

    private void AddUnderbarrelToCurrentLoadoutPrimary(GameObject underbarrel)
    {
        if (currentMenuLoadout.primaryData.firearmData.attachments.underbarrel.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.primaryData.firearmData.attachments.underbarrel.name == underbarrel.name)
            {
                Destroy(currentMenuLoadout.primaryData.firearmData.attachmentsPos.underbarrel.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.underbarrel.name = null;
                currentMenuLoadout.primaryData.firearmData.attachments.underbarrel.attachment = null;
                return;
            }
            else 
            {
                Destroy(currentMenuLoadout.primaryData.firearmData.attachmentsPos.underbarrel.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.primaryData.firearmData.attachments.underbarrel.attachment = Instantiate(underbarrel, currentMenuLoadout.primaryData.firearmData.attachmentsPos.underbarrel.pos);

        currentMenuLoadout.primaryData.firearmData.attachments.underbarrel.attachment.name = currentMenuLoadout.primaryData.firearmData.attachments.underbarrel.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.primaryData.firearmData.attachments.underbarrel.name = currentMenuLoadout.primaryData.firearmData.attachments.underbarrel.attachment.name;
    }

    private void AddUnderbarrelToCurrentLoadoutSecondary(GameObject underbarrel)
    {
        if (currentMenuLoadout.secondaryData.firearmData.attachments.underbarrel.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.secondaryData.firearmData.attachments.underbarrel.attachment.name == underbarrel.name)
            {
                Destroy(currentMenuLoadout.secondaryData.firearmData.attachmentsPos.underbarrel.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.secondaryData.firearmData.attachments.underbarrel.name = null;
                currentMenuLoadout.secondaryData.firearmData.attachments.underbarrel.attachment = null;
                return;
            }
            else 
            {
                Destroy(currentMenuLoadout.secondaryData.firearmData.attachmentsPos.underbarrel.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.secondaryData.firearmData.attachments.underbarrel.attachment = Instantiate(underbarrel, currentMenuLoadout.secondaryData.firearmData.attachmentsPos.underbarrel.pos);

        currentMenuLoadout.secondaryData.firearmData.attachments.underbarrel.attachment.name = currentMenuLoadout.secondaryData.firearmData.attachments.underbarrel.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.secondaryData.firearmData.attachments.underbarrel.name = currentMenuLoadout.secondaryData.firearmData.attachments.underbarrel.attachment.name;
    }

    #endregion

    private void OnWeaponTypeChange()
    {
        switch (currentWeaponType)
        {
            case WeaponType.Primary:
                currentMenuLoadout.secondaryData.firearmData.firearm.SetActive(false);
                currentMenuLoadout.meleeData.melee.SetActive(false);
                currentMenuLoadout.primaryData.firearmData.firearm.SetActive(true);
                break;
            case WeaponType.Secondary:
                currentMenuLoadout.meleeData.melee.SetActive(false);
                currentMenuLoadout.primaryData.firearmData.firearm.SetActive(false);
                currentMenuLoadout.secondaryData.firearmData.firearm.SetActive(true);
                break;
            case WeaponType.Melee:
                currentMenuLoadout.secondaryData.firearmData.firearm.SetActive(false);
                currentMenuLoadout.primaryData.firearmData.firearm.SetActive(false);
                currentMenuLoadout.meleeData.melee.SetActive(true);
                break;
        }
    }



    #region GENERATE_MENU_LOADOUT_WEAPONS

    private LoadoutData MenuClassLoadoutByUserDataLoadout(LoadoutData loadoutData) 
    {
        GameObject primary = GenerateMenuLoadoutClassPrimary(loadoutData.primaryData);
        AttachmentsPosData primaryAttachmentsPosData = GetAttachmentsPosData(primary);
        PrimaryData primaryData = new PrimaryData(primary, loadoutData.primaryData.type);
        primaryData.firearmData.attachmentsPos = primaryAttachmentsPosData;
        primary.SetActive(false);

        GameObject secondary = GenerateMenuLoadoutClassSecondary(loadoutData.secondaryData);
        AttachmentsPosData SecondaryAttachmentsPosData = GetAttachmentsPosData(secondary);
        SecondaryData secondaryData = new SecondaryData(secondary, loadoutData.secondaryData.type);
        secondaryData.firearmData.attachmentsPos = SecondaryAttachmentsPosData;
        secondary.SetActive(false);

        GameObject melee = GenerateMenuLoadoutClassMelee(loadoutData.meleeData);
        melee.SetActive(false);
        MeleeData meleeData = new MeleeData(melee, loadoutData.meleeData.type);

        return new LoadoutData(primaryData,secondaryData,meleeData);
    }

    private GameObject GenerateMenuLoadoutClassPrimary(MyDatatypes.Loadout.PrimaryData primaryData) 
    {
        GameObject primaryFirearm;
        switch (primaryData.type) 
        {
            case PrimaryFirearmType.Assault_Rifle:
                Object weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.assaultRifles[primaryData.firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.Battle_Rifle:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.battleRifles[primaryData.firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.Carbine:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.carbines[primaryData.firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.Shotgun:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.shotguns[primaryData.firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.PDW:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.pdws[primaryData.firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.DMR:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.dmrs[primaryData.firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.LMG:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.lmgs[primaryData.firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            default: // (PrimaryFirearmType.Sniper_Rifle)
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.sniperRifles[primaryData.firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
        }

        primaryFirearm.name = primaryFirearm.name.Replace("(Clone)", "");
        return primaryFirearm;
    }

    private GameObject GenerateMenuLoadoutClassSecondary(MyDatatypes.Loadout.SecondaryData secondaryData)
    {
        GameObject secondaryFirearm = null;
        Object weaponObj = null;

        switch (secondaryData.type) 
        {
            case SecondaryFirearmType.Pistols:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.pistols[secondaryData.firearmData.name])).menuPrefab;
                secondaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case SecondaryFirearmType.Machine_Pistols:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.machinePistols[secondaryData.firearmData.name])).menuPrefab;
                secondaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case SecondaryFirearmType.Revolvers:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.revolvers[secondaryData.firearmData.name])).menuPrefab;
                secondaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            default: // (SecondaryFirearmType.Other)
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.othersSecondary[secondaryData.firearmData.name])).menuPrefab;
                secondaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
        }

        secondaryFirearm.name = secondaryFirearm.name.Replace("(Clone)", "");
        return secondaryFirearm;
    }

    private GameObject GenerateMenuLoadoutClassMelee(MyDatatypes.Loadout.MeleeData meleeData) 
    {
        GameObject melee = null;
        Object meleeObj = null;

        switch (meleeData.type) 
        {
            case MeleeType.OneHandedBlade:
                meleeObj = ( (ResourcePrefabs)(LoadoutResources_Handler.Singleton.oneHandedBlade[meleeData.name]) ).menuPrefab;
                melee = Instantiate(meleeObj as GameObject, _menuWeaponParent);
                break;
            case MeleeType.TwoHandedBlade:
                meleeObj = ( (ResourcePrefabs)(LoadoutResources_Handler.Singleton.twoHandedBlade[meleeData.name]) ).menuPrefab;
                melee = Instantiate(meleeObj as GameObject, _menuWeaponParent);
                break;
            case MeleeType.OneHandedBlunt:
                meleeObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.oneHandedBlunt[meleeData.name])).menuPrefab;
                melee = Instantiate(meleeObj as GameObject, _menuWeaponParent);
                break;
            default: //(MeleeType.TwoHandedBlunt)
                meleeObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.twoHandedBlunt[meleeData.name])).menuPrefab;
                melee = Instantiate(meleeObj as GameObject, _menuWeaponParent);
                break;
        }

        melee.name = melee.name.Replace("(Clone)", "");
        return melee;
    }
    

    public AttachmentsPosData GetAttachmentsPosData(GameObject firearm)
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

    #endregion

    private void HandleInstance()
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
}
