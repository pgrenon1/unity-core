using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneIndex : OdinSerializedScriptableObject
{
    [SerializeField, Sirenix.OdinInspector.FilePath]
    public string bootstrapScenePath;

    [SerializeField, Sirenix.OdinInspector.FilePath]
    public string mainMenuScenePath;

    [SerializeField, Sirenix.OdinInspector.FilePath]
    public string gameScenePath;
    
    [SerializeField, Sirenix.OdinInspector.FilePath]
    public string emptyScenePath;

    public int EmptySceneBuildIndex
    {
        get
        {
            return SceneUtility.GetBuildIndexByScenePath(emptyScenePath);
        }
    }
    
    public int MainMenuSceneBuildIndex
    {
        get
        {
            return SceneUtility.GetBuildIndexByScenePath(mainMenuScenePath);
        }
    }

    public int BootStrapSceneBuildIndex
    {
        get
        {
            return SceneUtility.GetBuildIndexByScenePath(bootstrapScenePath);
        }
    }

    public int GameSceneBuildIndex
    {
        get
        {
            return SceneUtility.GetBuildIndexByScenePath(gameScenePath);
        }
    }
}
