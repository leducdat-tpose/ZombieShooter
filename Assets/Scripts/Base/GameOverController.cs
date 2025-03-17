using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private GameController _gameController;
    private GameManager _gameManager;
    private bool _isGameOver;
    public void Initialise(GameManager gameManager,GameController gameController)
    {
        _isGameOver = false;
        _gameManager = gameManager;
        _gameController = gameController;
        _gameController.OnPlayerDataChanged += CheckPlayerData;
    }
    private void CheckPlayerData(Player player)
    {
        if(_isGameOver) return;
        if(player.GetCurrentHealth() == 0)
        {
            _gameManager.GameOver("Lose");
            _isGameOver = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(_isGameOver) return;
        if(!other.CompareTag(Constant.PlayerTag)) return;
        _gameManager.GameOver("Victory");
        _isGameOver = true;
    }
}
