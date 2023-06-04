using System;

public class DictionaryLayerAttribute : Attribute
{
    public DictionaryAttributeTarget Target;
    public Type Attribute;

    public DictionaryLayerAttribute(DictionaryAttributeTarget target, Type attributeType)
    {
        Target = target;
        Attribute = attributeType;
    }
}
