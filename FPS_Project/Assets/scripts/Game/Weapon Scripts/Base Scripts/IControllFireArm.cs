using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllFireArm
{
    public void FireWeapon();
    public void StartAdsWeapon();
    public void StopAdsWeapon();
    public void NextAdsStateWeapon();
    public void ReloadWeapon();
}
