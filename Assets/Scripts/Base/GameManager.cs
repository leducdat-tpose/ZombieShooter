using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField]
    private GameObject _playerGameObject;
    [SerializeField]
    private Transform _playerSpawnPosition;
    [Header("References")]
    [SerializeField]
    private UIManager _uiManager;
    private GameController _gameController;
    [Header("Prefab PoolObject")]
    [SerializeField]
    private GameObject _playerProjectile;
    [SerializeField]
    private GameObject _monsterProjectile;
    [SerializeField]
    private GameObject _playerGrenadeProjectile;
    private void Awake() {
        ObjectPool.Instance.CreatePool(_playerProjectile, 10);
        ObjectPool.Instance.CreatePool(_monsterProjectile, 10);
        ObjectPool.Instance.CreatePool(_playerGrenadeProjectile, 5);
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
