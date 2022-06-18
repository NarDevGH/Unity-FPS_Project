using System.Collections;
using UnityEngine;

public class Firearm : MonoBehaviour
{
    public Firearm_Ads adsHandler { get; private set; }
    public Firearm_Recoil recoil_Handler { get; private set; }

    public bool CanFire { get; set; }
    public bool CanAds { get; set; }
    public bool CanReload { get; set; }
    public bool CanInspect { get; set; }


    protected int CurrentAmmoLoaded { get; set; }
    protected int MaxMagCapacity { get; set; }
    protected int CurrentTotalAmmo { get; set; }
    protected int MaxTotalAmmo { get; set; }
    protected float FireRate { get; set; }

    protected bool OnFireCooldown { get; set; }
    protected bool Reloading { get; set; }
    protected bool Aiming { get; set; }

    private float _fireCooldown => 1f / FireRate;


    protected virtual void Awake()
    {
        CanFire = true;
        CanAds = true;
        CanReload = true;
        CanInspect = true;

        Aiming = false;
        Reloading = false;


        adsHandler = gameObject.GetComponent<Firearm_Ads>();
        if (adsHandler) adsHandler.InitAdsHandler();

        recoil_Handler = gameObject.GetComponent<Firearm_Recoil>();
        if (recoil_Handler) recoil_Handler.InitFirearmRecoil();
    }

    #region FIRE_METHODS

    protected virtual bool Fire() {
        if (!CanFire) return false;
        if (FireRate <= 0) return false;
        if (CurrentAmmoLoaded <= 0) return false;
        if (OnFireCooldown) return false;

        return true;
    }

    protected void FireProjectile(GameObject projectile, Vector3 origin, Quaternion rotation, bool debugFire = true) 
    {
        StartFireCooldown();
        Instantiate(projectile, origin, rotation);

        if (debugFire) 
        {
            Debug.Log("Weapon Fired");
            Debug.DrawRay(origin,rotation*Vector3.forward,Color.blue,1);
        }
    }

    protected void FireRaycast(Vector3 origin, Vector3 direction, float maxDist = Mathf.Infinity, float damage = 1)
    {
        Debug.Log("Weapon Fired, Current Ammo Loaded: " + CurrentAmmoLoaded);

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxDist))
        {

            IDamageable lifeHandler = hit.transform.GetComponent<IDamageable>();

            if (lifeHandler != null)
            {
                lifeHandler.Damage(damage);
                return;
            }

            //if(enviroment)
            //  efecto de disparar al entorno

        }
    }

    protected void StartFireCooldown() { StartCoroutine(FireCooldownRoutine()); }

    private IEnumerator FireCooldownRoutine()
    {
        OnFireCooldown = true;
        Debug.Log("FireCooldown Start");
        
        float time = 0;
        while (time < _fireCooldown)
        {
            time += Time.deltaTime;
            yield return null;
        }

        OnFireCooldown = false;
        Debug.Log("FireCooldown End");
    }

    #endregion

    #region RELOAD METHODS

    protected virtual bool Reload() 
    {
        if (!CanReload) return false;
        if (CurrentAmmoLoaded >= MaxMagCapacity) return false;

        return true;
    }

    protected void ReloadAmmo_FullMag()
    {
        if (CurrentTotalAmmo == 0) return;

        int ammoReloaded = 0;
        int dif = MaxMagCapacity - CurrentAmmoLoaded;
        if (CurrentTotalAmmo >= dif)
        {
            CurrentTotalAmmo -= dif;
            ammoReloaded += dif;
        }
        else
        {
            CurrentTotalAmmo = 0;
            ammoReloaded += CurrentTotalAmmo;
        }

        CurrentAmmoLoaded = CurrentAmmoLoaded + ammoReloaded;
        Debug.Log("Ammo Reloaded, CurrentAmmo:" + CurrentAmmoLoaded + " of " + MaxMagCapacity + " / CurrentTotalAmmo:" + CurrentTotalAmmo + " of " + MaxTotalAmmo);
    }

    protected void ReloadAmmo_ByOne()
    {
        if (CurrentTotalAmmo <= 0) return;

        CurrentTotalAmmo -= 1;
        CurrentAmmoLoaded += 1;

        Debug.Log("Ammo Reloaded, CurrentAmmo:" + CurrentAmmoLoaded + " of " + MaxMagCapacity + " / CurrentTotalAmmo:" + CurrentTotalAmmo + " of " + MaxTotalAmmo);
    }

    #endregion

    #region ADS_METHODS

    protected virtual bool StartAds() 
    {
        if (!CanAds) return false;
        if (!adsHandler) return false;
        Aiming = true;
        return true;
    }
    protected virtual bool StopAds() 
    {
        if (!CanAds) return false;
        if (!adsHandler) return false;
        Aiming = false;
        return true;
    }
    protected virtual bool NextAdsState() 
    {
        if (!CanAds) return false;
        if (!adsHandler) return false;

        return true;
    }

    #endregion
}

