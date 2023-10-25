using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtilities;

public class AssetSceneManager
{
    public enum AssetScene
    {
        LoadingScene,
        LobbyScene,
        GameScene,
        CharacterSelectScene,
        MainMenuScene
    }
    
    
    private static string targetScene;
    
    public static void Exit()
    {
        Application.Quit();
    }
    public static void Tutorial()
    {
        //SceneManager.LoadNetworkScene("How To Play");
    }
    
    public static void Credit()
    {
        
    }

    public static void LoadScene(string sceneName)
    {
        targetScene = sceneName;
        SceneManager.LoadScene(AssetScene.LoadingScene.ToString());
    }
    
    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene);
    }
    
    public static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public static void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    
    public static void HomeScene()
    {
        SceneManager.LoadScene(0);
    }
    

}
