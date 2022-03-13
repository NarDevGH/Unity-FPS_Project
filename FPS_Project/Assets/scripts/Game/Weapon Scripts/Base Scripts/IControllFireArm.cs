using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllFireArm
{
    public void StartFireWeapon();
    public void StopFireWeapon();
    public void StartAdsWeapon();
    public void StopAdsWeapon();
    public void NextAdsStateWeapon();
    public void ReloadWeapon();
}
