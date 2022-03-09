using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match_Manager : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private GameObject _playerPrefab;

    public static Match_Manager Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else Destroy(this);
    }

    public void GeneratePlayer() 
    {
        GameObject player = Instantiate(_playerPrefab, _playerSpawn.position, _playerSpawn.rotation);
        GameObject primary = LoadoutResources_Handler.Singleton.InGamePrimaryGameObject(UserData.Singleton.currentClassLoadout.primaryData.name);
        GameObject secondary = LoadoutResources_Handler.Singleton.InGameSecondaryGameObject(UserData.Singleton.currentClassLoadout.secondaryData.name);
        GameObject melee = LoadoutResources_Handler.Singleton.InGameMeleeGameObject(UserData.Singleton.currentClassLoadout.meleeData.name);
        player.GetComponent<InGameLoadout_Handler>().GenerateInGameLoadout(primary, secondary, melee);
    }
}
