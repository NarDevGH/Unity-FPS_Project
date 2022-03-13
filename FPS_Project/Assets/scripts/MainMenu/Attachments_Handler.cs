using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attachments_Handler : MonoBehaviour
{
    [SerializeField] private Dropdown optic_dropdown;
    [SerializeField] private Dropdown barrel_dropdown;
    [SerializeField] private Dropdown underbarrel_dropdown;

    [SerializeField] private Menu_Weapon _weaponCreator;
    
    private List<Dropdown.OptionData> optic_Options = new List<Dropdown.OptionData>();
    private List<Dropdown.OptionData> barrel_Options = new List<Dropdown.OptionData>();
    private List<Dropdown.OptionData> underbarrel_Options = new List<Dropdown.OptionData>();
    private void Awake()
    {

            
        Object[] optics  = Resources.LoadAll("Prefabs/Menu/Attachments/Optic", typeof(GameObject));
        Object[] barrels  = Resources.LoadAll("Prefabs/Menu/Attachments/Barrel", typeof(GameObject));
        Object[] underbarrels  = Resources.LoadAll("Prefabs/Menu/Attachments/Underbarrel", typeof(GameObject));

        foreach (Object o in optics) 
        {
            optic_Options.Add(new Dropdown.OptionData(o.name));
        }
        foreach (Object o in barrels)
        {
            barrel_Options.Add(new Dropdown.OptionData(o.name));
        }
        foreach (Object o in underbarrels)
        {
            underbarrel_Options.Add(new Dropdown.OptionData(o.name));
        }

        optic_dropdown.options = optic_Options;
        barrel_dropdown.options = barrel_Options;
        underbarrel_dropdown.options = underbarrel_Options;
    }

    public void SelectOptic() 
    {
        _weaponCreator.AddOptic(Resources.Load("Prefabs/Menu/Attachments/Optic/"+optic_Options[optic_dropdown.value].text) as GameObject);
    }

    public void SelectBarrel()
    {
        _weaponCreator.AddBarrel(Resources.Load("Prefabs/Menu/Attachments/Barrel/" + barrel_Options[barrel_dropdown.value].text) as GameObject);
    }

    public void SelectUnderbarrel()
    {
        _weaponCreator.AddUnderbarrel(Resources.Load("Prefabs/Menu/Attachments/Underbarrel/" + underbarrel_Options[underbarrel_dropdown.value].text) as GameObject);
    }
}
