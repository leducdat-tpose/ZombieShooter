using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject _playerGameObject;
    [SerializeField]
    private Transform _playerSpawnPosition;
    [SerializeField]
    private UIManager _uiManager;
    private GameController _gameController;
    private void Awake() {
        _gameController = GameController.CreateAndInit(InitialisePlayer());
        _uiManager.Initialise(_gameController);
    }
    private Player InitialisePlayer()
    {
        GameObject playerObj = Instantiate(_playerGameObject, _playerSpawnPosition);
        playerObj.SetActive(false);
        if(playerObj.TryGetComponent<Player>(out Player player))
        {
            player.Initialise();
            playerObj.SetActive(true);
        }
        else Debug.LogError($"Initialise Player fail");
        return player;
    }
}
