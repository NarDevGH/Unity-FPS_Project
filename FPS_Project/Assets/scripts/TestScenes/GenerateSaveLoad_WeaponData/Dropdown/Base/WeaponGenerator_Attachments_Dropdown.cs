using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator_Attachments_Dropdown : MonoBehaviour
{
    [SerializeField, Min(0)] private float _spacing = 1f;
    [SerializeField] private GameObject _list;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private GameObject[] _items;

    static private List<WeaponGenerator_Attachments_Dropdown> dropdowns = new List<WeaponGenerator_Attachments_Dropdown>();

    private void Awake()
    {
        dropdowns.Add(this);
        _list.SetActive(false);

        GenerateItems(_items);
    }

    public void OnClick_Dropdown()
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

    private void GenerateItems(GameObject[] items)
    {
        if (items == null) return;

        float yPos = 0f;

        foreach (var item in items)
        {
            GameObject newItem = Instantiate(item, _itemContainer);
            newItem.name = newItem.name.Replace("(Clone)", "");

            newItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, yPos);

            float itemWHeight = item.GetComponent<RectTransform>().sizeDelta.y;
            yPos -= itemWHeight + _spacing;
        }

    }

    private static void CloseDropdownsExept(WeaponGenerator_Attachments_Dropdown currentDropdown)
    {
        foreach (WeaponGenerator_Attachments_Dropdown dropdown in dropdowns)
        {
            if (dropdown == currentDropdown) continue;

            dropdown._list.SetActive(false);
        }
    }
}
