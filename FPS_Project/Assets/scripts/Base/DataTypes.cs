using UnityEngine;


namespace MyDatatypes.UserData.Loadout
{
    public struct LoadoutData
    {
        public PrimaryData primaryData;
        public SecondaryData secondaryData;
        public MeleeData meleeData;


        public LoadoutData(PrimaryData primary, SecondaryData secondary, MeleeData melee)
        {
            this.primaryData = primary;
            this.secondaryData = secondary;
            this.meleeData = melee;
        }
    }

    public struct PrimaryData
    {
        public Firearm.FirearmData firearm;
        public MyDatatypes.Loadout.PrimaryFirearmType type;

        public PrimaryData(Firearm.FirearmData firearm, MyDatatypes.Loadout.PrimaryFirearmType type)
        {
            this.firearm = firearm;
            this.type = type;
        }
    }

    public struct SecondaryData
    {
        public Firearm.FirearmData firearm;
        public MyDatatypes.Loadout.SecondaryFirearmType type;

        public SecondaryData(Firearm.FirearmData firearm, MyDatatypes.Loadout.SecondaryFirearmType type)
        {
            this.firearm = firearm;
            this.type = type;
        }
    }

    public struct MeleeData
    {
        public string name;
        public MyDatatypes.Loadout.MeleeType type;


        public MeleeData(string name, MyDatatypes.Loadout.MeleeType type)
        {
            this.name = name;
            this.type = type;
        }
    }

    public struct FirearmData
    {
        public string name;
        public AttachmentsData attachments;

        public FirearmData(string name, AttachmentsData attachments)
        {
            this.name = name;
            this.attachments = attachments;
        }
    }

    public struct AttachmentsData
    {
        public AttachmentData optic;
        public AttachmentData barrel;
        public AttachmentData underbarrel;

        public AttachmentsData(AttachmentData optic, AttachmentData barrel, AttachmentData underbarrel)
        {
            this.optic = optic;
            this.barrel = barrel;
            this.underbarrel = underbarrel;
        }
    }

    public struct AttachmentData
    {
        public string name;

        public AttachmentData(string name)
        {
            this.name = name;
        }
    }
}


namespace MyDatatypes.GameManager
{
    public enum GameState {Playing,OnMenuScreen,OnLoadingScreen}
}

namespace MyDatatypes.GameResources 
{
    public struct ResourcePrefabs
    {
        public Object menuPrefab;
        public Object inGamePrefab;
        public ResourcePrefabs(Object menuPrefab, Object inGamePrefab)
        {
            this.menuPrefab = menuPrefab;
            this.inGamePrefab = inGamePrefab;
        }
    }
}

namespace MyDatatypes.Movement 
{
    [System.Serializable]
    public enum PoseState { Standing, Crouching, Prone }
    [System.Serializable]
    public enum MovementState {Still,Walking,Sprinting}

    [System.Serializable]
    public struct PoseData
    {
        public float height;
        public Vector3 center;
    }
}

namespace MyDatatypes.Firearm 
{
    [System.Serializable,HideInInspector]
    public struct FirearmData 
    {
        public string name;
        public GameObject firearm;
        public AttachmentsData attachments;
        public AttachmentsPosData attachmentsPos;

        public FirearmData(string name)
        {
            this.name = name;
            this.firearm = null;
            this.attachments = new AttachmentsData();
            this.attachmentsPos = new AttachmentsPosData();
        }

        public FirearmData(string name, GameObject firearm, AttachmentsData attachments, AttachmentsPosData attachmentsPos)
        {
            this.name = name;
            this.firearm = firearm;
            this.attachments = attachments;
            this.attachmentsPos = attachmentsPos;
        }
    }

    [System.Serializable, HideInInspector]
    public struct AttachmentsPosData 
    {
        public AttachmentPosData optic;
        public AttachmentPosData barrel;
        public AttachmentPosData underbarrel;

        public AttachmentsPosData(AttachmentPosData optic, AttachmentPosData barrel, AttachmentPosData underbarrel)
        {
            this.optic = optic;
            this.barrel = barrel;
            this.underbarrel = underbarrel;
        }
    }

    [System.Serializable, HideInInspector]
    public struct AttachmentPosData
    {
        public bool hasPos;
        public Transform pos;

        public AttachmentPosData(bool hasPos, Transform pos)
        {
            this.hasPos = hasPos;
            this.pos = pos;
        }
    }

    [System.Serializable, HideInInspector]
    public struct AttachmentsData 
    {
        public AttachmentData optic;
        public AttachmentData barrel;
        public AttachmentData underbarrel;

        public AttachmentsData(AttachmentData optic, AttachmentData barrel, AttachmentData underbarrel)
        {
            this.optic = optic;
            this.barrel = barrel;
            this.underbarrel = underbarrel;
        }
    }

    [System.Serializable, HideInInspector]
    public struct AttachmentData 
    {
        public string name;
        public GameObject attachment;

        public AttachmentData(string name, GameObject attachment)
        {
            this.name = name;
            this.attachment = attachment;
        }
    }
}

namespace MyDatatypes.Firearm.Ads 
{
    [System.Serializable]
    public struct AdsData
    {
        public float fov;
        public float swapSpeed;
        public Transform posTransform;
        public AdsDisplacement displacement { get; set; }
    }
    public struct AdsDisplacement
    {
        public Vector3 position_Displacement;
        public Vector3 eulerAngles;
    }
}

namespace MyDatatypes.Firearm.Recoil
{
    [System.Serializable]
    public struct FirearmRecoil 
    {
        public RecoilData weaponRecoil;
        public RecoilData headRecoil;
    }

    [System.Serializable]
    public struct RecoilData
    {
        public float recoverySpeed;
        public float snappiness;
        public RecoilAppliedData recoilApplied;
    }

    [System.Serializable]
    public class RecoilAppliedData
    {
        public Vector3 position;
        public Vector3 eulerAngles;

        public RecoilAppliedData()
        {
            position = Vector3.zero;
            eulerAngles = Vector3.zero;

        }

        public RecoilAppliedData(Transform transform)
        {
            position = transform.localPosition;
            eulerAngles = transform.localEulerAngles;

        }

        public void ResetData()
        {
            position = Vector3.zero;
            eulerAngles = Vector3.zero;
        }

        public void SetData(Vector3 position, Vector3 eulerangles)
        {
            this.position = position;
            this.eulerAngles = eulerangles;
        }
    }
}

namespace MyDatatypes.Loadout
{
    public enum LoadoutClass { Assault, Scout, Support, Reacon }
    public enum PrimaryFirearmType { Assault_Rifle, Battle_Rifle, Carbine, Shotgun, PDW, DMR, LMG, Sniper_Rifle }
    public enum SecondaryFirearmType { Pistols, Machine_Pistols, Revolvers, Others }
    public enum MeleeType { OneHandedBlade, OneHandedBlunt, TwoHandedBlade, TwoHandedBlunt }
    public enum WeaponType { Primary, Secondary, Melee}
    public enum AttachmentType { Optic, Barrel, Underbarrel, Other }

    public struct LoadoutData
    {
        public PrimaryData primaryData;
        public SecondaryData secondaryData;
        public MeleeData meleeData;

        public LoadoutData(PrimaryData primary, SecondaryData secondary, MeleeData melee)
        {
            this.primaryData = primary;
            this.secondaryData = secondary;
            this.meleeData = melee;
        }
    }

    public struct PrimaryData
    {
        public Firearm.FirearmData firearmData;
        public PrimaryFirearmType type;

        public PrimaryData(string name, PrimaryFirearmType type)
        {
            this.firearmData = new Firearm.FirearmData(name);
            this.type = type;
        }

        public PrimaryData(GameObject firearm, PrimaryFirearmType type)
        {
            Firearm.FirearmData newFirearm = new Firearm.FirearmData(firearm.name);
            newFirearm.firearm = firearm;
            this.firearmData = newFirearm;
            this.type = type;
        }

        public PrimaryData(Firearm.FirearmData firearm, PrimaryFirearmType type)
        {
            this.firearmData = firearm;
            this.type = type;
        }
    }

    public struct SecondaryData
    {
        public Firearm.FirearmData firearmData;
        public SecondaryFirearmType type;

        public SecondaryData(GameObject firearm, SecondaryFirearmType type)
        {
            Firearm.FirearmData  newFirearm= new Firearm.FirearmData(firearm.name);
            newFirearm.firearm = firearm;
            this.firearmData = newFirearm;
            this.type = type;
        }

        public SecondaryData(string name, SecondaryFirearmType type)
        {
            this.firearmData = new Firearm.FirearmData(name);
            this.type = type;
        }

        public SecondaryData(Firearm.FirearmData firearm, SecondaryFirearmType type)
        {
            this.firearmData = firearm;
            this.type = type;
        }
    }

    public struct MeleeData 
    {
        public string name;
        public GameObject melee;
        public MeleeType type;

        public MeleeData(string name, MeleeType type)
        {
            this.name = name;
            this.melee = null;
            this.type = type;
        }

        public MeleeData(GameObject melee, MeleeType type)
        {
            this.name = melee.name;
            this.melee = melee;
            this.type = type;
        }

        public MeleeData(string name, GameObject melee, MeleeType type)
        {
            this.name = name;
            this.melee = melee;
            this.type = type;
        }
    }

}



