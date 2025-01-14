using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        MainMenu,
        DojoScene
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene){

        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }

    public static void LoadNetwork(Scene targetScene){
        NetworkManager.Singleton.SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);
    }

    public static void LoaderCallback(){
        if (targetScene != Scene.MainMenu){
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}
