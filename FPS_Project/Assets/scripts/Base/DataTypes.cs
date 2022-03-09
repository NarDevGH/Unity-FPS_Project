using UnityEngine;

namespace MyDatatypes.TransformData
{
    [System.Serializable]
    public class TransformData
    {
        public Vector3 position;
        public Vector3 eulerAngles;
        public Vector3 local_Position;
        public Vector3 local_EulerAngles;

        public TransformData()
        {
            position = Vector3.zero;
            eulerAngles = Vector3.zero;
            local_Position = Vector3.zero;
            local_EulerAngles = Vector3.zero;

        }

        public TransformData(Transform t)
        {
            position = t.position;
            eulerAngles = t.eulerAngles;
            local_Position = t.localPosition;
            local_EulerAngles = t.localEulerAngles;
        }

        public void CopyTransformVariables(Transform t)
        {
            position = t.position;
            eulerAngles = t.eulerAngles;
            local_Position = t.localPosition;
            local_EulerAngles = t.localEulerAngles;
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

namespace MyDatatypes.Loadout
{
    public enum LoadoutClass { Assault, Scout, Support, Reacon }
    public enum PrimaryFirearmType { Assault_Rifle, Battle_Rifle, Carbine, Shotgun, PDW, DMR, LMG, Sniper_Rifle }
    public enum SecondaryFirearmType { Pistols, Machine_Pistols, Revolvers, Others }
    public enum MeleeType { OneHandedBlade, OneHandedBlunt, TwoHandedBlade, TwoHandedBlunt }
    public enum WeaponType { Primary, Secondary, Melee}
    public enum AttachmentType { Optic, Barrel, Underbarrel, Other }
}

namespace MyDatatypes.Loadout.UserData 
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
        public string name;
        public PrimaryFirearmType type;
        public AttachmentsData attachments;

        public PrimaryData(string name,PrimaryFirearmType type)
        {
            this.name = name;
            this.type = type;
            attachments = new AttachmentsData();
        }
    }

    public struct SecondaryData
    {
        public string name;
        public SecondaryFirearmType type;
        public AttachmentsData attachments;

        public SecondaryData(string name, SecondaryFirearmType type)
        {
            this.name = name;
            this.type = type;
            attachments = new AttachmentsData();
        }
    }

    public struct MeleeData 
    {
        public string name;
        public MeleeType type;

        public MeleeData(string name, MeleeType type)
        {
            this.name = name;
            this.type = type;
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
        public bool hasPos;

        public AttachmentData(string name, bool hasPos)
        {
            this.name = name;
            this.hasPos = hasPos;
        }
    }
}

namespace MyDatatypes.Loadout.Menu
{
    public struct LoadoutData
    {
        public PrimaryData primary;
        public SecondaryData secondary;
        public MeleeData melee;

        public LoadoutData(PrimaryData primary, SecondaryData secondary, MeleeData melee)
        {
            this.primary = primary;
            this.secondary = secondary;
            this.melee = melee;
        }
    }

    public struct PrimaryData
    {
        public GameObject firearm;
        public PrimaryFirearmType type;
        public Attachments attachments;

        public PrimaryData(GameObject firearm, PrimaryFirearmType type)
        {
            this.firearm = firearm;
            this.type = type;
            attachments = new Attachments();
        }

        public PrimaryData(GameObject firearm, PrimaryFirearmType type, Attachments attachments)
        {
            this.firearm = firearm;
            this.type = type;
            this.attachments = attachments;
        }
    }

    public struct SecondaryData
    {
        public GameObject firearm;
        public SecondaryFirearmType type;
        public Attachments attachments;

        public SecondaryData(GameObject firearm, SecondaryFirearmType type)
        {
            this.firearm = firearm;
            this.type = type;
            attachments = new Attachments();
        }
        public SecondaryData(GameObject firearm, SecondaryFirearmType type,Attachments attachments)
        {
            this.firearm = firearm;
            this.type = type;
            this.attachments = attachments;
        }
    }

    public struct MeleeData
    {
        public GameObject weapon;
        public MeleeType type;

        public MeleeData(GameObject weapon, MeleeType type)
        {
            this.weapon = weapon;
            this.type = type;
        }
    }

    public struct Attachments
    {
        public AttachmentData optic;
        public AttachmentData barrel;
        public AttachmentData underbarrel;
    }

    public struct AttachmentData
    {
        public bool hasPos;
        public Transform pos;
        public GameObject attachment;
    }
}

namespace MyDatatypes.Ads 
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
