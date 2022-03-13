using System.Collections.Generic;
using UnityEngine;
using MyDatatypes.Firearm.Ads;

public class Optic_Attachment : MonoBehaviour
{
    public List<AdsData> opticAdsStates => _opticAdsStates;

    [SerializeField] private List<AdsData> _opticAdsStates;
}
