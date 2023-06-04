using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[InlineProperty, HideReferenceObjectPicker]
public class Keyword
{
    [ValueDropdown("GetValues"), HideLabel, GUIColor("GetKeywordColor")]
    public string key;

    public Keyword()
    {
        
    }
    
    public Keyword(string key)
    {
        this.key = key;
    }

    public static KeywordConfig Config
    {
        get { return Index.Instance.keywordConfig;  }
    }
    
    public List<string> GetValues()
    {
        return Config.keywords;
    }

    public Color GetKeywordColor()
    {
        if (!IsValid())
            return Color.red;

        return Color.white;
    }

    public bool IsValid()
    {
        return GetValues().Contains(key);
    }

    public static implicit operator string(Keyword keyword)
    {
        return keyword.key;
    }
    
    public static implicit operator Keyword(string key)
    {
        return new Keyword(key);
    }
    
    //public override bool Equals(object obj)
    //{
    //    var keywordObj = (Keyword)obj;
    //    if (keywordObj != null)
    //        return keywordObj.keyword == keyword;
    //    else
    //        return base.Equals(obj);
    //}

    public override int GetHashCode()
    {
        return key.GetHashCode();
    }
}