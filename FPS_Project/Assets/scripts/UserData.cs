using UnityEngine;
using MyDatatypes.Loadout;

public class UserData : MonoBehaviour
{
    public LoadoutData currentClassLoadout;

    public LoadoutData assaultLoadout;
    public LoadoutData scoutLoadout;
    public LoadoutData supportLoadout;
    public LoadoutData reaconLoadout;
    public LoadoutClass currentClass 
    { 
        get { return _currentClass; }
        set 
        {
            if (value == _currentClass) return;
            switch (_currentClass) 
            {
                case LoadoutClass.Assault:
                    assaultLoadout = currentClassLoadout;
                    break;
                case LoadoutClass.Scout:
                    scoutLoadout = currentClassLoadout;
                    break;
                case LoadoutClass.Support:
                    supportLoadout = currentClassLoadout;
                    break;
                case LoadoutClass.Reacon:
                    reaconLoadout = currentClassLoadout;
                    break;
            }
            switch (value) 
            {
                case LoadoutClass.Assault:
                    currentClassLoadout = assaultLoadout;
                    break;
                case LoadoutClass.Scout:
                    currentClassLoadout = scoutLoadout;
                    break;
                case LoadoutClass.Support:
                    currentClassLoadout = supportLoadout;
                    break;
                case LoadoutClass.Reacon:
                    currentClassLoadout = reaconLoadout;
                    break;
            }
            _currentClass = value;
        }
    } 

    private LoadoutClass _currentClass = LoadoutClass.Assault;

    public static UserData Singleton;

    private void Awake()
    {
        HandleInstance();
        InitClassesData();
    }

    private void InitClassesData()
    {
        assaultLoadout = new LoadoutData(new PrimaryData("AK74",PrimaryFirearmType.Assault_Rifle), 
                                         new SecondaryData("M1911",SecondaryFirearmType.Pistols), 
                                         new MeleeData("Knife",MeleeType.OneHandedBlade) 
                                         );
        scoutLoadout = new LoadoutData(new PrimaryData("Uzi",PrimaryFirearmType.PDW),  
                                         new SecondaryData("M1911", SecondaryFirearmType.Pistols), 
                                         new MeleeData("Knife", MeleeType.OneHandedBlade) 
                                         );
        supportLoadout = new LoadoutData(new PrimaryData("M249",PrimaryFirearmType.LMG), 
                                         new SecondaryData("M1911", SecondaryFirearmType.Pistols), 
                                         new MeleeData("Knife", MeleeType.OneHandedBlade) 
                                         );
        reaconLoadout =  new LoadoutData(new PrimaryData("M107",PrimaryFirearmType.Sniper_Rifle), 
                                         new SecondaryData("M1911", SecondaryFirearmType.Pistols), 
                                         new MeleeData("Knife", MeleeType.OneHandedBlade) 
                                         );

        currentClassLoadout = assaultLoadout;
    }

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
