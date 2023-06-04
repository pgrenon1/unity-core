using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector.Editor;

public class DictionaryAttributeProcessor<T1, T2> : OdinAttributeProcessor<EditableKeyValuePair<T1, T2>>
{
    public override void ProcessChildMemberAttributes(InspectorProperty property, MemberInfo member, List<Attribute> attributes)
    {
        DictionaryLayerAttribute dictAttributerAttribute = property.GetAttribute<DictionaryLayerAttribute>();

        if (dictAttributerAttribute != null)
        {
            switch (dictAttributerAttribute.Target)
            {
                case DictionaryAttributeTarget.Key:
                    if (member.Name is "Key")
                        attributes.Add((Attribute)Activator.CreateInstance(dictAttributerAttribute.Attribute));
                    break;
                case DictionaryAttributeTarget.Value:
                    if (member.Name is "Value")
                        attributes.Add((Attribute)Activator.CreateInstance(dictAttributerAttribute.Attribute));
                    break;
                case DictionaryAttributeTarget.KeyAndValue:
                    if (member.Name is "Key" || member.Name is "Value")
                        attributes.Add((Attribute)Activator.CreateInstance(dictAttributerAttribute.Attribute));
                    break;
                default:
                    break;
            }
        }
    }
}