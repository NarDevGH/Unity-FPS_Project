using MyDatatypes.GameResources;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutResources_Dropdown : MonoBehaviour
{
    [SerializeField,Min(0)] private float _spacing;
    [SerializeField] private GameObject _list;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private GameObject _itemTemplate;

    static private List<LoadoutResources_Dropdown> _menuDropdowns = new List<LoadoutResources_Dropdown>();

    protected virtual void Awake() 
    {
        _menuDropdowns.Add(this);
        _list.SetActive(false);
    }

    public virtual void OnClick_Dropdown()
    {
        if (_list.activeSelf == false)
        {
            CloseDropdownsExept(this);
            _list.SetActive(true);
        }
        else 
        {
            _list.SetActive(false);
        }
    }

    protected void GenerateItems(Dictionary<string, ResourcePrefabs> resources)
    {
        if (resources == null) return;

        float yPos = 0f;
        float itemHeight = _itemTemplate.GetComponent<RectTransform>().sizeDelta.y;

        foreach (var resource in resources) 
        {
            GameObject newItem = Instantiate(_itemTemplate, _itemContainer);
            newItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -yPos);
            newItem.name = resource.Key + "_Btn";

            IDropdownItem dropdownItemButtonScript = newItem.GetComponent<IDropdownItem>();
            dropdownItemButtonScript.InitDropdownItem(resource.Key,((ResourcePrefabs)resource.Value).menuPrefab);

            yPos += itemHeight + _spacing;
        }

    }

    private static void CloseDropdownsExept(LoadoutResources_Dropdown currentDropdown) 
    {
        foreach (LoadoutResources_Dropdown dropdown in _menuDropdowns) 
        {
            if (dropdown == currentDropdown) continue;

            dropdown._list.SetActive(false);
        }
    }
}
