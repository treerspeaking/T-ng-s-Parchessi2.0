using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityUtilities;

namespace _Scripts.Managers.Persitence
{
    public class LoadingSceneManager : SingletonMonoBehaviour<LoadingSceneManager>
    {
        [SerializeField] private float _delayDuration = 1.5f;
        private bool _isFirstUpdate = true;
        private void Update()
        {
            if (_isFirstUpdate)
            {
                _isFirstUpdate = false;
                
                Invoke(nameof(DelayUnload),_delayDuration);
            }
        }
        
        public void DelayUnload()
        {
            SceneManager.UnloadSceneAsync(AssetSceneManager.AssetScene.LoadingScene.ToString());
        }
    }
}