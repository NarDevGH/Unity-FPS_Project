using System.Collections.Generic;
using UnityEngine;
using MyDatatypes.GameResources;
using MyDatatypes.Loadout;

public class LoadoutResources_Handler : MonoBehaviour
{
    private const string INGAME_LOADOUT_PATH = "Prefabs/InGame/";
    private const string MENU_LOADOUT_PATH = "Prefabs/Menu/";

    public bool resourcesFullyLoaded { get => _resourcesFullyLoaded; }

    //main weapons
    public Dictionary<string,ResourcePrefabs> assaultRifles { get; private set; }
    public Dictionary<string,ResourcePrefabs> battleRifles { get; private set; }
    public Dictionary<string,ResourcePrefabs> carbines { get; private set; }
    public Dictionary<string,ResourcePrefabs> shotguns { get; private set; }
    public Dictionary<string,ResourcePrefabs> pdws { get; private set; }
    public Dictionary<string,ResourcePrefabs> dmrs { get; private set; }
    public Dictionary<string,ResourcePrefabs> lmgs { get; private set; }
    public Dictionary<string, ResourcePrefabs> sniperRifles { get; private set; }

    //secondary weapons
    public Dictionary<string,ResourcePrefabs> pistols { get; private set; }
    public Dictionary<string,ResourcePrefabs> machinePistols { get; private set; }
    public Dictionary<string,ResourcePrefabs> revolvers { get; private set; }
    public Dictionary<string, ResourcePrefabs> othersSecondary { get; private set; }

    //melee weapons
    public Dictionary<string,ResourcePrefabs> oneHandedBlade { get; private set; }
    public Dictionary<string,ResourcePrefabs> twoHandedBlade { get; private set; }
    public Dictionary<string,ResourcePrefabs> oneHandedBlunt { get; private set; }
    public Dictionary<string, ResourcePrefabs> twoHandedBlunt { get; private set; }

    //attachments
    public Dictionary<string,ResourcePrefabs> optics { get; private set; }
    public Dictionary<string,ResourcePrefabs> barrels { get; private set; }
    public Dictionary<string,ResourcePrefabs> underbarrels { get; private set; }
    public Dictionary<string, ResourcePrefabs> othersAttachments { get; private set; }

    private bool _resourcesFullyLoaded;

    public static LoadoutResources_Handler Singleton;

    private void Awake()
    {
        HandleInstances();

        _resourcesFullyLoaded = false;

        GetMainWeaponsResources();
        GetSecondaryWeaponsResources();
        GetMeleeWeaponsResources();
        GetAttachmentsResources();

    }

    private void Start()
    {
        _resourcesFullyLoaded = true;
    }
    public GameObject MenuPrimaryGameObject(string name)
    {
        name = name.Replace("(Clone)", "");
        if (assaultRifles.ContainsKey(name))
        {
            return assaultRifles[name].menuPrefab as GameObject;
        }
        else if (battleRifles.ContainsKey(name))
        {
            return battleRifles[name].menuPrefab as GameObject;
        }
        else if (shotguns.ContainsKey(name))
        {
            return shotguns[name].menuPrefab as GameObject;
        }
        else if (carbines.ContainsKey(name))
        {
            return carbines[name].menuPrefab as GameObject;
        }
        else if (pdws.ContainsKey(name))
        {
            return pdws[name].menuPrefab as GameObject;
        }
        else if (dmrs.ContainsKey(name))
        {
            return dmrs[name].menuPrefab as GameObject;
        }
        else if (lmgs.ContainsKey(name))
        {
            return lmgs[name].menuPrefab as GameObject;
        }
        else if (sniperRifles.ContainsKey(name))
        {
            return sniperRifles[name].menuPrefab as GameObject;
        }

        return null;
    }

    public GameObject MenuSecondaryGameObject(string name)
    {
        name = name.Replace("(Clone)", "");
        if (pistols.ContainsKey(name))
        {
            return pistols[name].menuPrefab as GameObject;
        }
        else if (machinePistols.ContainsKey(name))
        {
            return machinePistols[name].menuPrefab as GameObject;
        }
        else if (revolvers.ContainsKey(name))
        {
            return revolvers[name].menuPrefab as GameObject;
        }
        else if (othersSecondary.ContainsKey(name))
        {
            return othersSecondary[name].menuPrefab as GameObject;
        }

        return null;
    }

    public GameObject InGamePrimaryGameObject(string name) 
    {
        name = name.Replace("(Clone)", "");
        if (assaultRifles.ContainsKey(name))
        {
            return assaultRifles[name].inGamePrefab as GameObject;
        }
        else if (battleRifles.ContainsKey(name))
        {
            return battleRifles[name].inGamePrefab as GameObject;
        }
        else if (shotguns.ContainsKey(name))
        {
            return shotguns[name].inGamePrefab as GameObject;
        }
        else if (carbines.ContainsKey(name))
        {
            return carbines[name].inGamePrefab as GameObject;
        }
        else if (pdws.ContainsKey(name))
        {
            return pdws[name].inGamePrefab as GameObject;
        }
        else if (dmrs.ContainsKey(name))
        {
            return dmrs[name].inGamePrefab as GameObject;
        }
        else if (lmgs.ContainsKey(name))
        {
            return lmgs[name].inGamePrefab as GameObject;
        }
        else if (sniperRifles.ContainsKey(name))
        {
            return sniperRifles[name].inGamePrefab as GameObject;
        }

        return null;
    }

    public GameObject InGameSecondaryGameObject(string name)
    {
        name = name.Replace("(Clone)", "");
        if (pistols.ContainsKey(name))
        {
            return pistols[name].inGamePrefab as GameObject;
        }
        else if (machinePistols.ContainsKey(name))
        {
            return machinePistols[name].inGamePrefab as GameObject;
        }
        else if (revolvers.ContainsKey(name))
        {
            return revolvers[name].inGamePrefab as GameObject;
        }
        else if (othersSecondary.ContainsKey(name))
        {
            return othersSecondary[name].inGamePrefab as GameObject;
        }

        return null;
    }

    public GameObject InGameMeleeGameObject(string name)
    {
        name = name.Replace("(Clone)", "");
        if (oneHandedBlade.ContainsKey(name))
        {
            return oneHandedBlade[name].inGamePrefab as GameObject;
        }
        else if (oneHandedBlunt.ContainsKey(name))
        {
            return oneHandedBlunt[name].inGamePrefab as GameObject;
        }
        else if (twoHandedBlade.ContainsKey(name))
        {
            return twoHandedBlade[name].inGamePrefab as GameObject;
        }
        else if (twoHandedBlunt.ContainsKey(name))
        {
            return twoHandedBlunt[name].inGamePrefab as GameObject;
        }

        return null;
    }

    public PrimaryFirearmType GetPrimaryType(GameObject firearm)
    {
        if (assaultRifles.ContainsKey(firearm.name)) return PrimaryFirearmType.Assault_Rifle;
        else if (battleRifles.ContainsKey(firearm.name)) return PrimaryFirearmType.Battle_Rifle;
        else if (carbines.ContainsKey(firearm.name)) return PrimaryFirearmType.Carbine;
        else if (shotguns.ContainsKey(firearm.name)) return PrimaryFirearmType.Shotgun;
        else if (pdws.ContainsKey(firearm.name)) return PrimaryFirearmType.PDW;
        else if (dmrs.ContainsKey(firearm.name)) return PrimaryFirearmType.DMR;
        else if (lmgs.ContainsKey(firearm.name)) return PrimaryFirearmType.LMG;
        else return PrimaryFirearmType.Sniper_Rifle;
    }

    public SecondaryFirearmType GetSecondaryType(GameObject firearm)
    {
        if (pistols.ContainsKey(firearm.name)) return SecondaryFirearmType.Pistols;
        else if (machinePistols.ContainsKey(firearm.name)) return SecondaryFirearmType.Machine_Pistols;
        else if (revolvers.ContainsKey(firearm.name)) return SecondaryFirearmType.Revolvers;
        else return SecondaryFirearmType.Others;
    }

    public MeleeType GetMeleeType(GameObject weapon)
    {
        if (oneHandedBlade.ContainsKey(weapon.name)) return MeleeType.OneHandedBlade;
        else if (twoHandedBlade.ContainsKey(weapon.name)) return MeleeType.TwoHandedBlade;
        else if (oneHandedBlade.ContainsKey(weapon.name)) return MeleeType.OneHandedBlunt;
        else return MeleeType.TwoHandedBlunt;
    }

    #region INIT_LOADOUT_RESOURCES_METHODS

    private void HandleInstances()
    {
        if (Singleton == null) { Singleton = this; }
        else Destroy(this);
    }

    private void GetMainWeaponsResources()
    {
        assaultRifles = GetTypeResources("Weapons/Primary/" + PrimaryFirearmType.Assault_Rifle.ToString());
        battleRifles = GetTypeResources("Weapons/Primary/" + PrimaryFirearmType.Battle_Rifle.ToString());
        carbines = GetTypeResources("Weapons/Primary/" + PrimaryFirearmType.Carbine.ToString());
        shotguns = GetTypeResources("Weapons/Primary/" + PrimaryFirearmType.Shotgun.ToString());
        pdws = GetTypeResources("Weapons/Primary/" + PrimaryFirearmType.PDW.ToString());
        dmrs = GetTypeResources("Weapons/Primary/" + PrimaryFirearmType.DMR.ToString());
        lmgs = GetTypeResources("Weapons/Primary/" + PrimaryFirearmType.LMG.ToString());
        sniperRifles = GetTypeResources("Weapons/Primary/" + PrimaryFirearmType.Sniper_Rifle.ToString());
    }

    private void GetSecondaryWeaponsResources()
    {
        pistols         = GetTypeResources("Weapons/Secondary/" + SecondaryFirearmType.Pistols);
        machinePistols  = GetTypeResources("Weapons/Secondary/" + SecondaryFirearmType.Machine_Pistols);
        revolvers       = GetTypeResources("Weapons/Secondary/" + SecondaryFirearmType.Revolvers);
        othersSecondary = GetTypeResources("Weapons/Secondary/" + SecondaryFirearmType.Others);
    }

    private void GetMeleeWeaponsResources()
    {
        oneHandedBlade = GetTypeResources("Weapons/Melee/" + MeleeType.OneHandedBlade);
        oneHandedBlunt = GetTypeResources("Weapons/Melee/" + MeleeType.OneHandedBlunt);
        twoHandedBlade = GetTypeResources("Weapons/Melee/" + MeleeType.TwoHandedBlade);
        twoHandedBlunt = GetTypeResources("Weapons/Melee/" + MeleeType.TwoHandedBlunt);
    }

    private void GetAttachmentsResources()
    {
        optics = GetTypeResources("Attachments/" + AttachmentType.Optic);
        barrels = GetTypeResources("Attachments/" + AttachmentType.Barrel);
        underbarrels = GetTypeResources("Attachments/" + AttachmentType.Underbarrel);
        othersAttachments = GetTypeResources("Attachments/" + AttachmentType.Other);
    }

    

    private Dictionary<string, ResourcePrefabs> GetTypeResources(string typePath) 
    {
        Dictionary<string, ResourcePrefabs> resourcesData = new Dictionary<string, ResourcePrefabs>(); ;

        Object[] menuWeapons = Resources.LoadAll( MENU_LOADOUT_PATH + typePath);
        Object[] inGameWeapons = Resources.LoadAll( INGAME_LOADOUT_PATH + typePath);

        if (menuWeapons.Length > 0) 
        {
            for (int i = 0; i < menuWeapons.Length; i++) 
            {
                int inGameWeaponIndex = InGameWeaponIndex(ref inGameWeapons,menuWeapons[i].name);
                if (inGameWeaponIndex != -1)
                {
                    resourcesData.Add(menuWeapons[i].name, new ResourcePrefabs(menuWeapons[i], inGameWeapons[inGameWeaponIndex]));
                }
                else 
                {
                    Debug.LogWarning("Couldnt find inGame Weapon Version of: " + menuWeapons[i].name);
                }
            }
        }

        return resourcesData;
    }

    private int InGameWeaponIndex(ref Object[] inGameWeapons, string name)
    {
        int index = -1;
        for (int i = 0; i < inGameWeapons.Length; i++)
        {
            if (inGameWeapons[i].name == name) 
            {
                return i;
            }
        }
        return index;
    }

    #endregion
}

