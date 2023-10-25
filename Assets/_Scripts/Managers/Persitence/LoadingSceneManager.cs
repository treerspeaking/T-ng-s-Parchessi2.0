using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtilities;

namespace _Scripts.Managers.Persitence
{
    public class LoadingSceneManager : SingletonMonoBehaviour<LoadingSceneManager>
    {
        private bool _isFirstUpdate = true;
        private void Update()
        {
            if (_isFirstUpdate)
            {
                _isFirstUpdate = false;
                SceneManager.UnloadSceneAsync(AssetSceneManager.AssetScene.LoadingScene.ToString());
            }
        }
    }
}