using UnityEngine;

public class SemiAutomatic_Firearm : Firearm, IControllFireArm
{
    [SerializeField, Min(0)] private int _maxTotalAmmo;
    [SerializeField, Min(0)] private int _maxMagCapacity;
    [SerializeField, Min(0)] private int _damage;
    [SerializeField, Min(0)] private int _firerate;

    private Camera _worldcamera;

    protected override void Awake()
    {
        base.Awake();

        MaxTotalAmmo = CurrentTotalAmmo = _maxTotalAmmo;
        MaxMagCapacity = CurrentAmmoLoaded = _maxMagCapacity;
        FireRate = _firerate;

        GetWorldCamera();
    }


    public void StartFireWeapon() => Fire();
    public void StopFireWeapon() { }

    public void StartAdsWeapon() => StartAds();
    public void StopAdsWeapon() => StopAds();
    public void NextAdsStateWeapon()
    {
        NextAdsState();
        if (Aiming == true) StartAds();
    }

    public void ReloadWeapon() => Reload();


    protected override bool Fire()
    {
        if (!base.Fire()) return false;

        CurrentAmmoLoaded--;
        FireRaycast(_worldcamera.transform.position, _worldcamera.transform.forward, _damage);

        if (recoil_Handler) {
            if (Aiming)
            {
                recoil_Handler.AimingFireRecoil();
            }
            else 
            {
                recoil_Handler.IdleFireRecoil();
            }
        }

        StartFireCooldown();
        return true;
    }

    protected override bool Reload()
    {
        if (!base.Reload()) return false;

        Reloading = true;
        ReloadAmmo_FullMag();
        Reloading = false;

        return true;
    }

    protected override bool StartAds()
    {
        if (!base.StartAds()) return false;

        adsHandler.StartAdsRoutine();
        return true;
    }

    protected override bool StopAds()
    {
        if (!base.StopAds()) return false;

        adsHandler.StartMoveTowardsIdleStateRoutine();
        return true;
    }

    protected override bool NextAdsState()
    {
        if (!base.NextAdsState()) return false;

        adsHandler.NextAdsState();
        return true;
    }

    private void GetWorldCamera()
    {
        var cam = FindObjectsOfType<Camera>(true);
        for (int i = 0; i < cam.Length; i++)
        {
            if (cam[i].name == "World_Camera")
            {
                _worldcamera = cam[i];
            }

        }
    }

    
}
