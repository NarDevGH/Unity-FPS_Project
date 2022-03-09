using UnityEngine;
using MyDatatypes.GameResources;
using MyDatatypes.Loadout;
using MyDatatypes.Loadout.Menu;

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
                currentMenuLoadout.primary.firearm.SetActive(false);
                break;
            case WeaponType.Secondary:
                currentMenuLoadout.secondary.firearm.SetActive(false);
                break;
            case WeaponType.Melee:
                currentMenuLoadout.melee.weapon.SetActive(false);
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
                currentMenuLoadout.primary.firearm.SetActive(true);
                break;
            case WeaponType.Secondary:
                currentMenuLoadout.secondary.firearm.SetActive(true);
                break;
            case WeaponType.Melee:
                currentMenuLoadout.melee.weapon.SetActive(true);
                break;
        } //enable current weaponType
    }

    


    #region SET_CURRENT_MENU_WEAPON_METHODS

    public void SetCurrentMenuWeapon(GameObject weapon) 
    {
        switch (currentWeaponType) 
        {
            case WeaponType.Primary:
                if (currentMenuLoadout.primary.firearm.name == weapon.name) return;

                SetCurrentMenuPrimary(weapon);
                break;

            case WeaponType.Secondary:
                if (currentMenuLoadout.secondary.firearm.name == weapon.name) return;

                SetCurrentMenuSecondary(weapon);
                break;

            case WeaponType.Melee:
                if (currentMenuLoadout.melee.weapon.name == weapon.name) return;

                SetCurrentMenuMelee(weapon);
                break;
        }
    }

    private void SetCurrentMenuPrimary(GameObject weapon)
    {
        Destroy(currentMenuLoadout.primary.firearm); // Destroy old weapon

        currentMenuLoadout.primary.firearm = Instantiate(weapon, _menuWeaponParent);
        currentMenuLoadout.primary.attachments = InitAttachmentsData(currentMenuLoadout.primary.firearm);

        currentMenuLoadout.primary.firearm.name = currentMenuLoadout.primary.firearm.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.primaryData.name = currentMenuLoadout.primary.firearm.name;
        UserData.Singleton.currentClassLoadout.primaryData.type = LoadoutResources_Handler.Singleton.GetPrimaryType(currentMenuLoadout.primary.firearm);
        UserData.Singleton.currentClassLoadout.primaryData.attachments = UserData.SerializeAttachments(currentMenuLoadout.primary.attachments);
    }

    private void SetCurrentMenuSecondary(GameObject weapon)
    {
        Destroy(currentMenuLoadout.secondary.firearm);

        currentMenuLoadout.secondary.firearm = Instantiate(weapon, _menuWeaponParent);
        currentMenuLoadout.secondary.attachments = InitAttachmentsData(currentMenuLoadout.secondary.firearm);

        currentMenuLoadout.secondary.firearm.name = currentMenuLoadout.secondary.firearm.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.secondaryData.name = currentMenuLoadout.secondary.firearm.name;
        UserData.Singleton.currentClassLoadout.secondaryData.type = LoadoutResources_Handler.Singleton.GetSecondaryType(currentMenuLoadout.secondary.firearm);
        UserData.Singleton.currentClassLoadout.secondaryData.attachments = UserData.SerializeAttachments(currentMenuLoadout.secondary.attachments);
    }

    private void SetCurrentMenuMelee(GameObject weapon)
    {
        Destroy(currentMenuLoadout.melee.weapon);

        currentMenuLoadout.melee.weapon = Instantiate(weapon, _menuWeaponParent);

        currentMenuLoadout.melee.weapon.name = currentMenuLoadout.melee.weapon.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.meleeData.name = currentMenuLoadout.melee.weapon.name;
        UserData.Singleton.currentClassLoadout.meleeData.type = LoadoutResources_Handler.Singleton.GetMeleeType(currentMenuLoadout.melee.weapon);
    }

    #endregion


    #region ADD_OPTIC_METHODS

    public void AddOptic(GameObject optic) 
    {
        switch (_currentWeaponType)
        {
            case WeaponType.Primary:
                if (currentMenuLoadout.primary.attachments.optic.hasPos == false) return;

                AddOpticToCurrentLoadoutPrimary(optic);
                break;

            case WeaponType.Secondary:
                if (currentMenuLoadout.secondary.attachments.optic.hasPos == false) return;

                AddOpticToCurrentLoadoutSecondary(optic);
                break;
        }
    }

    private void AddOpticToCurrentLoadoutPrimary(GameObject optic)
    {
        if (currentMenuLoadout.primary.attachments.optic.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.primary.attachments.optic.attachment.name == optic.name)
            {
                Destroy(currentMenuLoadout.primary.attachments.optic.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.primaryData.attachments.optic.name = null;
                currentMenuLoadout.primary.attachments.optic.attachment = null;
                return;
            }
            else //Destroy old Optic
            {
                Destroy(currentMenuLoadout.primary.attachments.optic.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.primary.attachments.optic.attachment = Instantiate(optic,currentMenuLoadout.primary.attachments.optic.pos);

        currentMenuLoadout.primary.attachments.optic.attachment.name = currentMenuLoadout.primary.attachments.optic.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.primaryData.attachments.optic.name = currentMenuLoadout.primary.attachments.optic.attachment.name;
    }

    private void AddOpticToCurrentLoadoutSecondary(GameObject optic)
    {
        
        if (currentMenuLoadout.secondary.attachments.optic.attachment != null)
        {   //on selected again the same attachment, remove it
            if (currentMenuLoadout.secondary.attachments.optic.attachment.name == optic.name)
            {
                Destroy(currentMenuLoadout.secondary.attachments.optic.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.secondaryData.attachments.optic.name = null;
                currentMenuLoadout.secondary.attachments.optic.attachment = null;
                return;
            }
            else //Destroy old Optic
            {
                Destroy(currentMenuLoadout.secondary.attachments.optic.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.secondary.attachments.optic.attachment = Instantiate(optic,currentMenuLoadout.secondary.attachments.optic.pos);

        currentMenuLoadout.secondary.attachments.optic.attachment.name = currentMenuLoadout.secondary.attachments.optic.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.secondaryData.attachments.optic.name = currentMenuLoadout.secondary.attachments.optic.attachment.name;
    }

    #endregion

    #region ADD_BARREL_METHODS

    public void AddBarrel(GameObject barrel)
    {
        switch (_currentWeaponType)
        {
            case WeaponType.Primary:
                if (currentMenuLoadout.primary.attachments.barrel.hasPos == false) return;

                AddBarrelToCurrentLoadoutPrimary(barrel);
                break;

            case WeaponType.Secondary:
                if (currentMenuLoadout.secondary.attachments.barrel.hasPos == false) return;

                AddBarrelToCurrentLoadoutSecondary(barrel);
                break;
        }
    }

    private void AddBarrelToCurrentLoadoutPrimary(GameObject barrel)
    {
        if (currentMenuLoadout.primary.attachments.barrel.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.primary.attachments.barrel.attachment.name == barrel.name)
            {
                Destroy(currentMenuLoadout.primary.attachments.barrel.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.primaryData.attachments.barrel.name = null;
                currentMenuLoadout.primary.attachments.barrel.attachment = null;
                return;
            }
            else //Destroy old Berrel 
            {
                Destroy(currentMenuLoadout.primary.attachments.barrel.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.primary.attachments.barrel.attachment = Instantiate(barrel, currentMenuLoadout.primary.attachments.barrel.pos);

        currentMenuLoadout.primary.attachments.barrel.attachment.name = currentMenuLoadout.primary.attachments.barrel.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.primaryData.attachments.barrel.name = currentMenuLoadout.primary.attachments.barrel.attachment.name;
    }

    private void AddBarrelToCurrentLoadoutSecondary(GameObject barrel)
    {
        if (currentMenuLoadout.secondary.attachments.barrel.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.secondary.attachments.barrel.attachment.name == barrel.name)
            {
                Destroy(currentMenuLoadout.secondary.attachments.barrel.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.secondaryData.attachments.barrel.name = null;
                currentMenuLoadout.secondary.attachments.barrel.attachment = null;
                return;
            }
            else 
            {
                Destroy(currentMenuLoadout.secondary.attachments.barrel.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.secondary.attachments.barrel.attachment = Instantiate(barrel, currentMenuLoadout.secondary.attachments.barrel.pos);

        currentMenuLoadout.secondary.attachments.barrel.attachment.name = currentMenuLoadout.secondary.attachments.barrel.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.secondaryData.attachments.barrel.name = currentMenuLoadout.secondary.attachments.barrel.attachment.name;
    }

    #endregion

    #region ADD_UNDERBARREL_METHODS

    public void AddUnderbarrel(GameObject underbarrel)
    {
        switch (_currentWeaponType)
        {
            case WeaponType.Primary:
                if (currentMenuLoadout.primary.attachments.underbarrel.hasPos == false) return;

                AddUnderbarrelToCurrentLoadoutPrimary(underbarrel);
                break;

            case WeaponType.Secondary:
                if (currentMenuLoadout.secondary.attachments.underbarrel.hasPos == false) return;

                AddUnderbarrelToCurrentLoadoutSecondary(underbarrel);
                break;
        }
    }

    private void AddUnderbarrelToCurrentLoadoutPrimary(GameObject underbarrel)
    {
        if (currentMenuLoadout.primary.attachments.underbarrel.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.primary.attachments.underbarrel.attachment.name == underbarrel.name)
            {
                Destroy(currentMenuLoadout.primary.attachments.underbarrel.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.primaryData.attachments.underbarrel.name = null;
                currentMenuLoadout.primary.attachments.underbarrel.attachment = null;
                return;
            }
            else 
            {
                Destroy(currentMenuLoadout.primary.attachments.underbarrel.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.primary.attachments.underbarrel.attachment = Instantiate(underbarrel, currentMenuLoadout.primary.attachments.underbarrel.pos);

        currentMenuLoadout.primary.attachments.underbarrel.attachment.name = currentMenuLoadout.primary.attachments.underbarrel.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.primaryData.attachments.underbarrel.name = currentMenuLoadout.primary.attachments.underbarrel.attachment.name;
    }

    private void AddUnderbarrelToCurrentLoadoutSecondary(GameObject underbarrel)
    {
        if (currentMenuLoadout.secondary.attachments.underbarrel.attachment != null) 
        {
            //on selected again the same attachment, remove it
            if (currentMenuLoadout.secondary.attachments.underbarrel.attachment.name == underbarrel.name)
            {
                Destroy(currentMenuLoadout.secondary.attachments.underbarrel.pos.GetChild(0).gameObject);
                UserData.Singleton.currentClassLoadout.secondaryData.attachments.underbarrel.name = null;
                currentMenuLoadout.secondary.attachments.underbarrel.attachment = null;
                return;
            }
            else 
            {
                Destroy(currentMenuLoadout.secondary.attachments.underbarrel.pos.GetChild(0).gameObject);
            }
        }

        currentMenuLoadout.secondary.attachments.underbarrel.attachment = Instantiate(underbarrel, currentMenuLoadout.secondary.attachments.underbarrel.pos);

        currentMenuLoadout.secondary.attachments.underbarrel.attachment.name = currentMenuLoadout.secondary.attachments.underbarrel.attachment.name.Replace("(Clone)", "");
        UserData.Singleton.currentClassLoadout.secondaryData.attachments.underbarrel.name = currentMenuLoadout.secondary.attachments.underbarrel.attachment.name;
    }

    #endregion

    private void OnWeaponTypeChange()
    {
        switch (currentWeaponType)
        {
            case WeaponType.Primary:
                currentMenuLoadout.secondary.firearm.SetActive(false);
                currentMenuLoadout.melee.weapon.SetActive(false);
                currentMenuLoadout.primary.firearm.SetActive(true);
                break;
            case WeaponType.Secondary:
                currentMenuLoadout.melee.weapon.SetActive(false);
                currentMenuLoadout.primary.firearm.SetActive(false);
                currentMenuLoadout.secondary.firearm.SetActive(true);
                break;
            case WeaponType.Melee:
                currentMenuLoadout.secondary.firearm.SetActive(false);
                currentMenuLoadout.primary.firearm.SetActive(false);
                currentMenuLoadout.melee.weapon.SetActive(true);
                break;
        }
    }



    #region GENERATE_MENU_LOADOUT_WEAPONS

    private LoadoutData MenuClassLoadoutByUserDataLoadout(MyDatatypes.Loadout.UserData.LoadoutData loadoutData) 
    {
        GameObject primary = GenerateMenuLoadoutClassPrimary(loadoutData.primaryData);
        primary.SetActive(false);
        Attachments primaryAttachmentsData = InitAttachmentsData(primary);
        PrimaryData primaryData = new PrimaryData(primary, loadoutData.primaryData.type, primaryAttachmentsData);

        GameObject secondary = GenerateMenuLoadoutClassSecondary(loadoutData.secondaryData);
        secondary.SetActive(false);
        Attachments SecondaryAttachmentsData = InitAttachmentsData(secondary);
        SecondaryData secondaryData = new SecondaryData(secondary, loadoutData.secondaryData.type, SecondaryAttachmentsData);

        GameObject melee = GenerateMenuLoadoutClassMelee(loadoutData.meleeData);
        melee.SetActive(false);
        MeleeData meleeData = new MeleeData(melee, loadoutData.meleeData.type);

        return new LoadoutData(primaryData,secondaryData,meleeData);
    }

    private GameObject GenerateMenuLoadoutClassPrimary(MyDatatypes.Loadout.UserData.PrimaryData firearmData) 
    {
        GameObject primaryFirearm;
        switch (firearmData.type) 
        {
            case PrimaryFirearmType.Assault_Rifle:
                Object weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.assaultRifles[firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.Battle_Rifle:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.battleRifles[firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.Carbine:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.carbines[firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.Shotgun:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.shotguns[firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.PDW:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.pdws[firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.DMR:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.dmrs[firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case PrimaryFirearmType.LMG:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.lmgs[firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            default: // (PrimaryFirearmType.Sniper_Rifle)
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.sniperRifles[firearmData.name])).menuPrefab;
                primaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
        }

        primaryFirearm.name = primaryFirearm.name.Replace("(Clone)", "");
        return primaryFirearm;
    }

    private GameObject GenerateMenuLoadoutClassSecondary(MyDatatypes.Loadout.UserData.SecondaryData firearmData)
    {
        GameObject secondaryFirearm = null;
        Object weaponObj = null;

        switch (firearmData.type) 
        {
            case SecondaryFirearmType.Pistols:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.pistols[firearmData.name])).menuPrefab;
                secondaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case SecondaryFirearmType.Machine_Pistols:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.machinePistols[firearmData.name])).menuPrefab;
                secondaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            case SecondaryFirearmType.Revolvers:
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.revolvers[firearmData.name])).menuPrefab;
                secondaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
            default: // (SecondaryFirearmType.Other)
                weaponObj = ((ResourcePrefabs)(LoadoutResources_Handler.Singleton.othersSecondary[firearmData.name])).menuPrefab;
                secondaryFirearm = Instantiate(weaponObj as GameObject, _menuWeaponParent);
                break;
        }

        secondaryFirearm.name = secondaryFirearm.name.Replace("(Clone)", "");
        return secondaryFirearm;
    }

    private GameObject GenerateMenuLoadoutClassMelee(MyDatatypes.Loadout.UserData.MeleeData meleeData) 
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
    

    public Attachments InitAttachmentsData(GameObject firearm)
    {
        Attachments attachmentsData = new Attachments();

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
