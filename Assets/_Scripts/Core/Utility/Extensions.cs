using System;
using DG.Tweening;
using Sirenix.Utilities;
using System.Collections.Generic;
#if UNITY_EDITOR
#endif
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class Extensions 
{
    public static IEnumerable<Enum> GetFlags(this Enum input)
    {
        foreach (Enum value in Enum.GetValues(input.GetType()))
            if (input.HasFlag(value))
                yield return value;
    }
    
    public static float Remap (this float value, float from1, float to1, float from2, float to2) 
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
    public static Vector3 RandomPointInBounds(this Bounds bounds) {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public static void AddRangeUnique<T>(this ICollection<T> collection, ICollection<T> elements)
    {
        foreach (var element in elements)
        {
            collection.AddUnique(element);
        }
    }

    public static T RandomElement<T>(this List<T> collection, bool removeAfter = false)
    {
        var randomIndex = Random.Range(0, collection.Count);
        var element = collection[randomIndex];

        if (removeAfter)
            collection.Remove(element);

        return element;
    } 

    public static DOTweenAnimation GetDOTweenAnimationByID(this GameObject gameObject, string id)
    {
        foreach (var animation in gameObject.GetComponents<DOTweenAnimation>())
        {
            if (animation.id == id)
                return animation;
        }

        return null;
    }

    public static DOTweenAnimation GetDOTweenAnimationByID(this Component component, string id)
    {
        return component.GetDOTweenAnimationByID(id);
    }

    public static void AddUnique<T>(this ICollection<T> collection, T element)
    {
        if (collection.Contains(element))
            return;

        collection.Add(element);
    }

    public static void OrderByDistance(this RaycastHit[] hitInfos)
    {
        hitInfos.Sort((x, y) => x.distance.CompareTo(y.distance));
    }

    public static void SetNavigationMode(this Selectable selectable, Navigation.Mode mode)
    {
        var nav = selectable.navigation;
        nav.mode = mode;
        selectable.navigation = nav;
    }

    public static void SetDefaultExplicitNavigation(this Selectable selectable)
    {
        var nav = selectable.navigation;
        nav.mode = Navigation.Mode.Explicit;
        nav.selectOnUp = selectable.FindSelectableOnUp();
        nav.selectOnDown = selectable.FindSelectableOnDown();
        nav.selectOnLeft = selectable.FindSelectableOnLeft();
        nav.selectOnRight = selectable.FindSelectableOnRight();
        selectable.navigation = nav;
    }

    public static void SetNavigation(this Selectable selectable, Selectable onUp, Selectable onDown, Selectable onLeft, Selectable onRight)
    {
        var nav = selectable.navigation;
        nav.mode = Navigation.Mode.Explicit;
        nav.selectOnUp = onUp;
        nav.selectOnDown = onDown;
        nav.selectOnLeft = onLeft;
        nav.selectOnRight = onRight;
        selectable.navigation = nav;
    }

    public static void SetNavigationUp(this Selectable selectable, Selectable onUp)
    {
        var nav = selectable.navigation;
        nav.selectOnUp = onUp;
        selectable.navigation = nav;
    }

    public static void SetNavigationDown(this Selectable selectable, Selectable onDown)
    {
        var nav = selectable.navigation;
        nav.selectOnDown = onDown;
        selectable.navigation = nav;
    }

    public static void SetNavigationLeft(this Selectable selectable, Selectable onLeft)
    {
        var nav = selectable.navigation;
        nav.selectOnLeft = onLeft;
        selectable.navigation = nav;
    }

    public static void SetNavigationRight(this Selectable selectable, Selectable onRight)
    {
        var nav = selectable.navigation;
        nav.selectOnRight = onRight;
        selectable.navigation = nav;
    }

    public static string GetPath(this Transform transform)
    {
        string path = transform.name;
        while (transform.parent != null)
        {
            transform = transform.parent;
            path = transform.name + "/" + path;
        }
        return path;
    }
}
