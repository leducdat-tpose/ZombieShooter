using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button _startGameBtn;
    private void Awake() {
        _startGameBtn.onClick.AddListener(() => Loader.LoadScene(Loader.SceneType.GameScene));
    }
}
