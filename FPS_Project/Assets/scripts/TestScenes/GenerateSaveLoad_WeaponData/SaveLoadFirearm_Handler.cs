using System.IO;
using UnityEngine;
using UnityEngine.UI;
using MyDatatypes.Firearm;

public class SaveLoadFirearm_Handler : MonoBehaviour
{
    private string dataPath;

    private string FirearmDataPath => dataPath + _pathInputfield.text;


    [SerializeField] private GameObject _pathPanel;
    [SerializeField] private InputField _pathInputfield;
    [SerializeField] private Button _okBtn;
    [SerializeField] private Button _cancelBtn;

    private enum OpenMode { Saving, Loading }
    private OpenMode _currentOpenMode;

    private void Start()
    {
        _pathPanel.SetActive(false);
        dataPath = Application.dataPath + "/Resources/Data/";
    }

    public void SaveWeaponData()
    {
        _currentOpenMode = OpenMode.Saving;
        _pathPanel.SetActive(true);
    }

    public void LoadWeaponData()
    {
        _currentOpenMode = OpenMode.Loading;
        _pathPanel.SetActive(true);
    }

    public void OnOKButtonPressed()
    {
        switch (_currentOpenMode)
        {
            case OpenMode.Saving:
                SaveData();
                break;
            case OpenMode.Loading:
                LoadData();
                break;
        }
        _pathInputfield.text = "";
        _pathPanel.SetActive(false);
    }

    public void OnCancelButtonPressed()
    {
        _pathInputfield.text = "";
        _pathPanel.SetActive(false);
    }

    private void SaveData()
    {
        using (StreamWriter stream = new StreamWriter(FirearmDataPath))
        {
            FirearmData fireamrData = FirearmGenerator_Handler.singleton.currentFirearm;
            string json = JsonUtility.ToJson(fireamrData);
            stream.Write(json);
        }
        Debug.Log("Firearm Saved as: " + FirearmDataPath);
    }

    private void LoadData() 
    {
        using (StreamReader stream = new StreamReader(FirearmDataPath)) 
        {
            string json = stream.ReadToEnd();
            FirearmGenerator_Handler.singleton.GenerateWeapon(JsonUtility.FromJson<FirearmData>(json));
        }
        Debug.Log("Loaded Firearm: " + FirearmDataPath);
    }
}
