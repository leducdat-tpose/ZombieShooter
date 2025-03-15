using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderCallback : MonoBehaviour
{
    [SerializeField]
    private Image _loadingSlideBar;
    private bool _isFirstUpdate = true;
    private void Start() {
        _loadingSlideBar.fillAmount = 0;
    }
    private void Update() {
        if(_isFirstUpdate)
        {
        _isFirstUpdate = false;
        Loader.OnLoaderCallBack();
        return;
        }
        _loadingSlideBar.fillAmount = Loader.GetLoadingProgress();
    }
}
