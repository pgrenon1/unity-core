using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

[HideMonoScript]
public class OdinSerializedSingletonBehaviour<T> : OdinSerializedBehaviour where T : OdinSerializedBehaviour
{
    #region  Fields
    [CanBeNull]
    private static T _instance;

    [NotNull]
    // ReSharper disable once StaticMemberInGenericType
    private static readonly object Lock = new object();

    [SerializeField]
    private bool _dontDestroyOnLoad = true;
    #endregion

    #region  Properties
    
    public static bool Quitting { get; private set; }
    
    [NotNull]
    public static T Instance
    {
        get
        {
            if (Quitting)
            {
                Debug.LogWarning($"[{nameof(OdinSerializedSingletonBehaviour<T>)}<{typeof(T)}>] Instance will not be returned because the application is quitting.");
                // ReSharper disable once AssignNullToNotNullAttribute
                return null;
            }
            lock (Lock)
            {
                if (_instance != null)
                    return _instance;
                var instances = FindObjectsOfType<T>();
                var count = instances.Length;
                if (count > 0)
                {
                    if (count == 1)
                        return _instance = instances[0];
                    Debug.LogWarning($"[{nameof(OdinSerializedSingletonBehaviour<T>)}<{typeof(T)}>] There should never be more than one {nameof(OdinSerializedSingletonBehaviour<T>)} of type {typeof(T)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
                    for (var i = 1; i < instances.Length; i++)
                        Destroy(instances[i]);
                    return _instance = instances[0];
                }

                Debug.Log($"[{nameof(OdinSerializedSingletonBehaviour<T>)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                return _instance = new GameObject($"({nameof(OdinSerializedSingletonBehaviour<T>)}){typeof(T)}")
                           .AddComponent<T>();
            }
        }
    }
    #endregion

    #region  Methods
    protected virtual void Awake()
    {
        if (_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
    
    private void OnApplicationQuit()
    {
        Quitting = true;
    }
    #endregion
}