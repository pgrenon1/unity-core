using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class EditorSceneOrderer
{
	const string activeSceneIndexKey = "ActiveSceneIndex";
	private static bool _shouldRemoveBootstrap = false;

	static EditorSceneOrderer()
	{
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
	}

	private static void OnPlayModeStateChanged(PlayModeStateChange targetState)
	{
		if (targetState == PlayModeStateChange.ExitingEditMode)
		{
			EditorPrefs.SetInt(activeSceneIndexKey, GetActiveSceneIndex());

			Scene bootstrapScene = SceneManager.GetSceneByBuildIndex(Index.Instance.sceneIndex.BootStrapSceneBuildIndex);
			if (!bootstrapScene.IsValid())
			{
#if UNITY_EDITOR
				bootstrapScene = EditorSceneManager.OpenScene(Index.Instance.sceneIndex.bootstrapScenePath, OpenSceneMode.Additive);
#else
				bootstrapScene = SceneManager.LoadScene(Index.Instance.sceneIndex.BootStrapSceneBuildIndex, LoadSceneMode.Additive);
#endif
				_shouldRemoveBootstrap = true;
			}

			// EditorSceneManager.MoveSceneBefore(bootstrapScene, SceneManager.GetSceneAt(0));
			SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(Index.Instance.sceneIndex.BootStrapSceneBuildIndex));
		}

		if (targetState == PlayModeStateChange.EnteredEditMode)
		{
			if (EditorPrefs.HasKey(activeSceneIndexKey))
			{
				SetActiveSceneIndex(EditorPrefs.GetInt(activeSceneIndexKey, 0));

				if (_shouldRemoveBootstrap)
                {
					for (int i = 0; i < SceneManager.sceneCount; i++)
					{
						var scene = SceneManager.GetSceneAt(i);

						if (scene.buildIndex == Index.Instance.sceneIndex.BootStrapSceneBuildIndex)
							EditorSceneManager.CloseScene(scene, true);
					}

					_shouldRemoveBootstrap = false;
				}
			}
		}
	}

    private static int GetActiveSceneIndex()
	{
		for (var i = 0; i < SceneManager.sceneCount; i++)
		{
			if (SceneManager.GetSceneAt(i) == SceneManager.GetActiveScene())
			{
				return i;
			}
		}

		return -1;
	}

	private static bool SetActiveSceneIndex(int index)
	{
		if (index < SceneManager.sceneCount)
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneAt(index));
			return true;
		}

		return false;
	}
}