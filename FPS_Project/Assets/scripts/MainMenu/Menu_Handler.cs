using System;
using System.Collections;
using UnityEngine;

public class Menu_Handler : MonoBehaviour
{
    [SerializeField] private MainPanelsInfo _mainMenu;
    [SerializeField] private MainPanelsInfo _loadout;
    [SerializeField] private MainPanelsInfo _attachments;
    [SerializeField] private MainPanelsInfo _settings;
    
    [SerializeField] private float _camSpeed;
    [SerializeField] private Transform _menuCam;
    [Space]
    [SerializeField] private GameObject _menu;

    private MenuPanel _currentPanel;
    private MainPanelsInfo _currentPanelInfo;

    private Coroutine _changePanelRoutine;

    [Serializable]
    private struct MainPanelsInfo 
    {
        public GameObject panel;
        public Transform camPos;
    }

    private enum MenuPanel {MainMenu,Loadout,Attachments,Setting };

    private void Start()
    {
        _menuCam.position = _mainMenu.camPos.position;
        _currentPanelInfo = _mainMenu;
    }

    public void OnDeploy() 
    {
        Match_Manager.Instance.GeneratePlayer();
        _menu.SetActive(false);
    }

    public void GoToLoadoutPanel() 
    {
        _currentPanel = MenuPanel.Loadout;

        _changePanelRoutine = StartCoroutine(ChangePanelRoutine(_currentPanelInfo.panel, _loadout.panel, _loadout.camPos));
    }

    public void GoToAttachentsPanel()
    {
        if (Menu_Weapon.Singleton.currentWeaponType == MyDatatypes.Loadout.WeaponType.Melee) return;

        _currentPanel = MenuPanel.Attachments;

        _changePanelRoutine = StartCoroutine(ChangePanelRoutine(_currentPanelInfo.panel, _attachments.panel, _attachments.camPos));
    }

    public void GoToMainMenuPanel()
    {
        _currentPanel = MenuPanel.MainMenu;

        _changePanelRoutine = StartCoroutine(ChangePanelRoutine(_currentPanelInfo.panel, _mainMenu.panel, _mainMenu.camPos));
    }

    private IEnumerator ChangePanelRoutine(GameObject currentPanel, GameObject nextPanel, Transform nextCamPos)
    {
        currentPanel.SetActive(false);
        while (_menuCam.position != nextCamPos.position)
        {
            _menuCam.position = Vector3.MoveTowards(_menuCam.position, nextCamPos.position, _camSpeed * Time.deltaTime);
            yield return null;
        }
        nextPanel.SetActive(true);
        _changePanelRoutine = null;

        switch (_currentPanel) 
        {
            case MenuPanel.MainMenu:
                _currentPanelInfo = _mainMenu;
                break;
            case MenuPanel.Loadout:
                _currentPanelInfo = _loadout;
                break;
            case MenuPanel.Attachments:
                _currentPanelInfo = _attachments;
                break;
            case MenuPanel.Setting:
                _currentPanelInfo = _settings;
                break;
        }
    }
}
