using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    private CinemachineVirtualCamera _virtualCamera;
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
    [SerializeField]
    private GameOverController _gameOverController;
    private void Awake() {
        ObjectPool.Instance.CreatePool(_playerProjectile, 10);
        ObjectPool.Instance.CreatePool(_monsterProjectile, 10);
        ObjectPool.Instance.CreatePool(_playerGrenadeProjectile, 5);
        Player player = InitialisePlayer();
        _virtualCamera.Follow = player.transform;
        _gameController = GameController.CreateAndInit(player);
        _uiManager.Initialise(_gameController);
        _gameOverController.Initialise(this, _gameController);
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
    public void GameOver(string text)
    {
        _uiManager.DisplayGameOver(text);
    }
}
