using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : OdinSerializedSingletonBehaviour<GameManager>
{
    public bool debugStateMachine;
    public bool debugStateTransitions;

    public PushdownStateMachine PushdownStateMachine { get; private set; } = new PushdownStateMachine();

    public GameState GameState { get; private set; }
    public int TimescaleIndex { get; set; }

    private bool _gameIsPaused;
    public bool GameIsPaused
    {
        get
        {
            return _gameIsPaused;
        }
        set
        {
            _gameIsPaused = value;

            if (_gameIsPaused)
            {
                ChangeTimeScaleIndex(0);
                ToggleCursorConfined(false);

                foreach (Player player in PlayerManager.Instance.Players)
                {
                    player.PlayerInputReader.ToggleMainGameInputs(false);
                }
            }
            else
            {
                ChangeTimeScaleIndex(3);
                ToggleCursorConfined(true);
                
                foreach (Player player in PlayerManager.Instance.Players)
                {
                    player.PlayerInputReader.ToggleMainGameInputs(true);
                }
            }
        }
    }

    private LoadingScreen _loadingScreenCache;
    public LoadingScreen LoadingScreen => GetCachedComponentInChildren(ref _loadingScreenCache);
    
    private PlayerActions _gameManagerPlayerActions;
    private List<float> _timescales = new List<float>() { 0f, 0.2f, 0.5f, 1f, 1.5f, 2f };

    protected override void Awake()
    {
        base.Awake();

        InitStates();

        TimescaleIndex = _timescales.IndexOf(1f);

        LoadingScreen.gameObject.SetActive(true);
    }

    public void ToggleLoadingScreen(bool show, Action onComplete = null)
    {
        if (show)
        {
            LoadingScreen.Show(onComplete);
        }
        else
        {
            LoadingScreen.Hide(onComplete);
        }
    }

    private void Start()
    {
        StartGame();

        _gameManagerPlayerActions = new PlayerActions();
        _gameManagerPlayerActions.Enable();
    }

    private void StartGame()
    {
        if (SceneManager.sceneCount == 1)
        {
            LoadGameMenuSync();
        }

        StartCoroutine(DoToggleLoadingScreen(false));
    }

    private void Update()
    {
        PushdownStateMachine.Update();
    }

    public void TogglePause(Player player = null)
    {
        GameIsPaused = !GameIsPaused;
    }

    public void ChangeTimeScaleIndex(int index)
    {
        TimescaleIndex = Mathf.Clamp(index, 0, _timescales.Count - 1);
        SetTimescale(_timescales[TimescaleIndex]);
    }

    private void SetTimescale(float timescale)
    {
        Time.timeScale = timescale;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#else
        Application.Quit();
#endif
    }

    #region State Machine
    private void InitStates()
    {
        GameState = new GameState(PushdownStateMachine);
    }

    public void GoToMatch()
    {
        PushdownStateMachine.ChangeState(GameState);
    }

    public List<Scene> GetNonBootstrapLoadedScenes()
    {
        List<Scene> scenes = new List<Scene>();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);

            if (scene.buildIndex == Index.Instance.sceneIndex.BootStrapSceneBuildIndex)
                continue;

            scenes.Add(scene);
        }

        return scenes;
    }

    public void GoBack()
    {
        PushdownStateMachine.ChangeStateBack();
    }

    public void ChangeState(State newState)
    {
        PushdownStateMachine.ChangeState(newState);
    }

    #endregion

    #region Scene Management
    public IEnumerator DoToggleLoadingScreen(bool show)
    {
        var isDone = false;

        if (show)
        {
            LoadingScreen.Show();
            LoadingScreen.ShowComplete += () => { isDone = true; };
        }
        else
        {
            LoadingScreen.Hide();
            LoadingScreen.HideComplete += () => { isDone = true; };
        }

        while (!isDone)
        {
            yield return null;
        }
    }

    public void LoadGameMenuSync()
    {
        int mainMenuSceneBuildIndex = Index.Instance.sceneIndex.MainMenuSceneBuildIndex;

        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(mainMenuSceneBuildIndex, LoadSceneMode.Additive);
        
        asyncOperation.completed += (operation) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(mainMenuSceneBuildIndex));
        };
    }

    public void SetActiveScene(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
    }

    public void LoadGameScene(List<Scene> scenesToUnload = null)
    {
        var buildIndex = Index.Instance.sceneIndex.GameSceneBuildIndex;

        StartCoroutine(DoLoadGameScene(buildIndex, scenesToUnload));
    }

    /// <summary>
    /// 1. show loading screen
    /// 2. [async] unload current scenes if needed
    /// 3. [async] load Level scene
    /// 4. init Level
    /// 5. hide loading screen
    /// 6. start Level
    /// </summary>
    public IEnumerator DoLoadGameScene(int levelSceneBuildIndex, List<Scene> scenesToUnload = null)
    {
        // show loading screen
        yield return StartCoroutine(DoToggleLoadingScreen(true));

        List<AsyncOperation> unloadOperations = new List<AsyncOperation>();
        if (scenesToUnload != null)
        {
            for (int i = 0; i < scenesToUnload.Count; i++)
            {
                unloadOperations.Add(UnloadSceneAsync(scenesToUnload[i]));
            }
        }

        var loadingSceneAsync = LoadSceneAsync(levelSceneBuildIndex);

        while (!unloadOperations.TrueForAll(op => op.isDone) || !loadingSceneAsync.isDone)
        {
            var debugString = "";
            
            foreach (var unloadOperation in unloadOperations)
            {
                debugString += $"Unload: {unloadOperation.progress} \n";
            }

            debugString += $"Load: {loadingSceneAsync.progress}";

            if (debugStateTransitions) Debug.Log(debugString);

            yield return null;
        }

        // setting active scene
        var loadedGameSceneIndex = SceneManager.GetSceneByBuildIndex(levelSceneBuildIndex);
        SceneManager.SetActiveScene(loadedGameSceneIndex);

        GameManager.Instance.ToggleLoadingScreen(false);
    }

    //public void UnloadLevel()
    //{
    //    StartCoroutine(DoUnloadLevel());
    //}

    ///// <summary>
    ///// 1. show loading screen
    ///// 2. [async] unload current Level scene if needed
    ///// 3. [async] load GameMenu scene
    ///// 4. init GameMenu
    ///// 5. hide loading screen
    ///// 6. start GameMenu
    ///// </summary>
    //private IEnumerator DoUnloadLevel()
    //{
    //    // show loading screen
    //    yield return StartCoroutine(DoToggleLoadingScreen(true));

    //    PlayerManager.Instance.DestroyRopeControllers();

    //    // if we have a level to unload, unload it
    //    var levelSceneNullable = GetLoadedLevelScene();

    //    AsyncOperation unloadLevelSceneAsync = null;
    //    if (levelSceneNullable != null)
    //    {
    //        unloadLevelSceneAsync = UnloadSceneAsync(levelSceneNullable.scene);
    //    }

    //    // load game menu in additive mode
    //    var loadGameMenuSceneAsync = LoadSceneAsync(Index.Instance.SceneIndex.GameMenuSceneBuildIndex, LoadSceneMode.Additive);

    //    while (!unloadLevelSceneAsync.isDone || !loadGameMenuSceneAsync.isDone)
    //    {
    //        //Debug.Log("Unload: " + unloadLevelSceneAsync.progress + "\n"
    //        //        + "Load: " + loadGameMenuSceneAsync.progress);
    //        yield return null;
    //    }

    //    // setting active scene
    //    var loadedLevelSceneIndex = SceneManager.GetSceneAt(1);
    //    SceneManager.SetActiveScene(loadedLevelSceneIndex);

    //    yield return StartCoroutine(DoToggleLoadingScreen(false));
    //}

    //private SceneNullable GetLoadedLevelScene()
    //{
    //    var level = FindObjectOfType<Level>();

    //    if (level)
    //    {
    //        return new SceneNullable(level.gameObject.scene);
    //    }

    //    return null;
    //}

    private AsyncOperation UnloadSceneAsync(Scene scene)
    {
        return SceneManager.UnloadSceneAsync(scene);
    }

    private AsyncOperation LoadSceneAsync(int sceneBuildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
    {
        return SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneMode);
    }
    #endregion

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.alignment = TextAnchor.UpperLeft;

        if (EventSystem.current == null)
            return;

        var currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected)
            GUI.Label(new Rect(0, 0, 200, 25), currentSelected.name);

        GUIContent content = new GUIContent();

        if (debugStateMachine)
        {
            content.text = "StateStack: \n";

            foreach (var state in PushdownStateMachine.StateStack)
            {
                content.text += "\t" + state + "\n";
            }
            GUI.Box(new Rect(0, 50, 250, 100), content, style);
        }
    }

    public void ToggleCursorConfined(bool confined)
    {
        if (confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}