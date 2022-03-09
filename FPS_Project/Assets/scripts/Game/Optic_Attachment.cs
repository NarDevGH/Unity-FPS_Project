using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDatatypes.Ads;

public class Optic_Attachment : MonoBehaviour
{
    public List<AdsData> opticAdsStates => _opticAdsStates;

    [SerializeField] private List<AdsData> _opticAdsStates;
}
