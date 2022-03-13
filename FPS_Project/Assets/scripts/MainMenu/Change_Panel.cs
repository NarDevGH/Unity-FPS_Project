using System.Collections;
using UnityEngine;

public class Change_Panel : MonoBehaviour
{
    [SerializeField] private GameObject _currentPanel;
    [SerializeField] private GameObject _nextPanel;

    [SerializeField] private Transform _nextCamPos;
    [SerializeField] private float _camSpeed;
    [SerializeField] private Transform _menuCam;

    public void GoToPanel() 
    {
        StartCoroutine(GoToNextPanelRoutine());
    }

    private IEnumerator GoToNextPanelRoutine() 
    {
        _currentPanel.SetActive(false);
        while (_menuCam.position != _nextCamPos.position) 
        {
            _menuCam.position = Vector3.MoveTowards(_menuCam.position, _nextCamPos.position, _camSpeed*Time.deltaTime);
            yield return null;
        }
        _nextPanel.SetActive(true);
    }
}
