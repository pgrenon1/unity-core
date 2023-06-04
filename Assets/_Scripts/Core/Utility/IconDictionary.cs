using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class IconDictionary : OdinSerializedScriptableObject
{
    public Dictionary<IconType, Sprite> icons = new Dictionary<IconType, Sprite>();

    public Sprite GetIcon(IconType iconType)
    {
        if (icons.ContainsKey(iconType))
        {
            return icons[iconType];
        }
        else
        {
            return icons[IconType.None];
        }
    }
}
