using MyDatatypes.GameResources;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Custom_MenuDropdown : MonoBehaviour
{
    [SerializeField,Min(0)] private float _spacing;
    [SerializeField] private GameObject _list;
    [SerializeField] private Transform _buttonsContainer;
    [SerializeField] private GameObject _buttonTemplate;

    static private List<Custom_MenuDropdown> _menuDropdowns = new List<Custom_MenuDropdown>();

    protected virtual void Awake() 
    {
        _menuDropdowns.Add(this);
        _list.SetActive(false);
    }

    protected virtual void OnClick_Dropdown()
    {
        CloseDropdownsExept(this);
        _list.SetActive(!_list.activeSelf);
    }

    protected void GenerateButtons(Dictionary<string, ResourcePrefabs> resources)
    {
        if (resources == null) return;

        float weaponButtonHeight = _buttonTemplate.GetComponent<RectTransform>().sizeDelta.y;


        float yPos = 0f;

        foreach (var resource in resources) 
        {
            GameObject newButton = Instantiate(_buttonTemplate, _buttonsContainer);
            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -yPos);
            newButton.name = resource.Key + "_Btn";

            ICustom_MenuDropdown_Item dropdownItemButtonScript = newButton.GetComponent<ICustom_MenuDropdown_Item>();
            dropdownItemButtonScript.InitDropdownItem(resource.Key,((ResourcePrefabs)resource.Value).menuPrefab);
            newButton.GetComponent<Button>().onClick.AddListener(dropdownItemButtonScript.OnButtonClick);

            yPos += weaponButtonHeight + _spacing;
        }

    }

    private static void CloseDropdownsExept(Custom_MenuDropdown currentDropdown) 
    {
        foreach (Custom_MenuDropdown dropdown in _menuDropdowns) 
        {
            if (dropdown == currentDropdown) continue;

            dropdown._list.SetActive(false);
        }
    }
}
