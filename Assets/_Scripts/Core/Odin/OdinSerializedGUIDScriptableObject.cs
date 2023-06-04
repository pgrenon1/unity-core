using Sirenix.OdinInspector;

public class OdinSerializedGUIDScriptableObject : OdinSerializedScriptableObject
{
    [ReadOnly]
    public string ID = System.Guid.NewGuid().ToString();

#if UNITY_EDITOR
    [Button("Regenerate GUID")]
    private void RegenerateGUID()
    {
        ID = System.Guid.NewGuid().ToString();
    }
#endif
}
