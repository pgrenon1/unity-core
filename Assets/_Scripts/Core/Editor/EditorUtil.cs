using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class EditorUtil
{
    [MenuItem("Tools/Scene Navigation/Add Bootstrap Scene")]
    public static void AddBootstrapScene()
    {
        Utils.LoadSceneEditor(Index.Instance.sceneIndex.bootstrapScenePath, OpenSceneMode.Additive);
    }

    private static List<Scene> GetScenesToUnload(int sceneToKeepBuildIndex)
    {
        var scenes = new List<Scene>();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);

            var bootstrapBuildIndex = Index.Instance.sceneIndex.BootStrapSceneBuildIndex;

            if (scene.buildIndex != bootstrapBuildIndex && scene.buildIndex != sceneToKeepBuildIndex)
                scenes.Add(scene);
        }

        return scenes;
    }

    [MenuItem("Tools/Scene Navigation/Load Main Menu Scene")]
    public static void LoadGameMenuScene()
    {
        LoadSceneWithBootstrap(Index.Instance.sceneIndex.mainMenuScenePath);
    }

    public static void LoadSceneWithBootstrap(string pathOfSceneToLoad)
    {
        Utils.LoadSceneEditor(Index.Instance.sceneIndex.bootstrapScenePath, OpenSceneMode.Single);

        Utils.LoadSceneEditor(pathOfSceneToLoad, OpenSceneMode.Additive);
    }

    private static void UnloadScenes(List<Scene> scenesToUnload)
    {
        for (int i = 0; i < scenesToUnload.Count; i++)
        {
            SceneManager.UnloadSceneAsync(scenesToUnload[i]);
        }
    }
}