using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtilities;

namespace _Scripts.Managers.Network
{
    public static class AssetNetworkSceneManager 
    {
        public static bool LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            Debug.Log($"Network about to load scene {sceneName} ");
            var status = NetworkManager.Singleton.SceneManager.LoadScene(sceneName, loadSceneMode);
            
            if (status != SceneEventProgressStatus.Started)
            {
                Debug.LogWarning($"Failed to load {sceneName} " +
                                 $"with a {nameof(SceneEventProgressStatus)}: {status}");
                return false;
            }
            else
            {
                Debug.Log($"Successfully load scene {sceneName} ");
                return true;
            }
        }

        public static bool LoadNextScene(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            var index =  SceneManager.GetActiveScene().buildIndex;
            var sceneName = SceneManager.GetSceneByBuildIndex(index + 1).name;
            if (sceneName == null)
            {
                Debug.LogWarning($"Failed to load next scene after {SceneManager.GetActiveScene().name} " +
                                 $"with a {nameof(SceneEventProgressStatus)}: {SceneEventProgressStatus.InvalidSceneName}");
                return false;
            }
            
            var status = NetworkManager.Singleton.SceneManager.LoadScene(sceneName, loadSceneMode);
            
            if (status != SceneEventProgressStatus.Started)
            {
                Debug.LogWarning($"Failed to load {sceneName} " +
                                 $"with a {nameof(SceneEventProgressStatus)}: {status}");
                return false;
            }
            else
            {
                Debug.Log($"Successfully load scene {sceneName} ");
                return true;
            }
        }
        
    }
}