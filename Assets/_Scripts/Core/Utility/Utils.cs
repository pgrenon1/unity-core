using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public static class Utils
{
    public static string NicifyVariable(string input)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        char[] inputArray = input.ToCharArray();
        int startIndex = 0;
     
        if(inputArray.Length > 1 && inputArray[0] == 'm' && input[1] == '_')
            startIndex += 2;
     
        if(inputArray.Length > 1 && inputArray[0] == 'k' && inputArray[1] >= 'A' && inputArray[1] <= 'Z')
            startIndex += 1;
     
        if(inputArray.Length > 0 && inputArray[0] >= 'a' && inputArray[0] <= 'z')
            inputArray[0] -= (char)('a' - 'A');

        output.Append(SplitCamelCase(new string(inputArray)));
     
        return output.ToString().TrimStart(' ');
    }
    
    public static string SplitCamelCase(string input)
    {
        return Regex.Replace(input, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled).Trim();
    }
    
    public static Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref Quaternion deriv, float smoothTime) {
        if (Time.deltaTime < Mathf.Epsilon) 
            return current;
        // account for double-cover
        var dot = Quaternion.Dot(current, target);
        var multi = dot > 0f ? 1f : -1f;
        target.x *= multi;
        target.y *= multi;
        target.z *= multi;
        target.w *= multi;
        // smooth damp (nlerp approx)
        var result = new Vector4(
            Mathf.SmoothDamp(current.x, target.x, ref deriv.x, smoothTime),
            Mathf.SmoothDamp(current.y, target.y, ref deriv.y, smoothTime),
            Mathf.SmoothDamp(current.z, target.z, ref deriv.z, smoothTime),
            Mathf.SmoothDamp(current.w, target.w, ref deriv.w, smoothTime)
        ).normalized;
		
        // ensure deriv is tangent
        var derivError = Vector4.Project(new Vector4(deriv.x, deriv.y, deriv.z, deriv.w), result);
        deriv.x -= derivError.x;
        deriv.y -= derivError.y;
        deriv.z -= derivError.z;
        deriv.w -= derivError.w;		
		
        return new Quaternion(result.x, result.y, result.z, result.w);
    }
    
    public static Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime)
    {
        Vector3 c = current.eulerAngles;
        Vector3 t = target.eulerAngles;
        return Quaternion.Euler(
            Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime),
            Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime),
            Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime)
        );
    }
        
    public static Vector3 FindClosestPointOnSphere(Vector3 sphereCenter, float sphereRadius, Vector3 point)
    {
        Vector3 direction = point - sphereCenter;

        return sphereCenter + direction.normalized * sphereRadius;
    }
    
    public static Vector3 GetAngleNoise(float min, float max)  
    {
        // Find random angle between min & max inclusive
        float xNoise = Random.Range (min, max);
        float yNoise = Random.Range (min, max);
        float zNoise = Random.Range (min, max);
 
        // Convert Angle to Vector3
        Vector3 noise = new Vector3 ( 
            Mathf.Sin (2 * Mathf.PI * xNoise /360), 
            Mathf.Sin (2 * Mathf.PI * yNoise /360), 
            Mathf.Sin (2 * Mathf.PI * zNoise /360)
        );
        
        return noise;
    }
    
    public static Vector3 GetPointOnUnitSphereCap(Quaternion targetDirection, float maxAngle, float minAngle = 0)
    {
        var angleInRad = UnityEngine.Random.Range(minAngle / 2, maxAngle / 2) * Mathf.Deg2Rad;
        var pointOnCircle = (UnityEngine.Random.insideUnitCircle.normalized) * Mathf.Sin(angleInRad);
        var offset = new Vector3(pointOnCircle.x, pointOnCircle.y, Mathf.Cos(angleInRad));
        return targetDirection * offset;
    }
    
    public static Vector3 ClampMagnitude(Vector3 vector, float min, float max)
    {
        double sqrMagnitude = vector.sqrMagnitude;
        if (sqrMagnitude > max * (double)max) return vector.normalized * max;
        if (sqrMagnitude < min * (double)min) return vector.normalized * min;
        return vector;
    }
    
    public static Vector3 GetMeanVector(List<Vector3> positions)
    {
        if (positions.Count == 0)
            return Vector3.zero;

        float x = 0f;
        float y = 0f;
        float z = 0f;

        foreach (Vector3 pos in positions)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }

        return new Vector3(x / positions.Count, y / positions.Count, z / positions.Count);
    }

    public static void SimulateClick(Button button)
    {
        ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
    }

    private static void SetCollisionIgnores(Collider collider, List<Collider> collidersToIgnore)
    {
        foreach (var colliderToIgnore in collidersToIgnore)
        {
            Physics.IgnoreCollision(collider, colliderToIgnore);
        }
    }

    // circleA is the center of the first circle, with radius radiusA
    // circleB is the center of the second circle, with radius radiusB
    public static int Intersect(Vector3 circleA,
                                float radiusA,
                                Vector3 circleB,
                                float radiusB,
                                out Vector3[] intersections)
    {

        float centerDx = circleA.x - circleB.x;
        float centerDy = circleA.z - circleB.z;
        float r = Mathf.Sqrt(centerDx * centerDx + centerDy * centerDy);

        // no intersection
        if (!(Mathf.Abs(radiusA - radiusB) <= r && r <= radiusA + radiusB))
        {
            intersections = new Vector3[0];
            return 0;
        }

        float r2d = r * r;
        float r4d = r2d * r2d;
        float rASquared = radiusA * radiusA;
        float rBSquared = radiusB * radiusB;
        float a = (rASquared - rBSquared) / (2 * r2d);
        float r2r2 = (rASquared - rBSquared);
        float c = Mathf.Sqrt(2 * (rASquared + rBSquared) / r2d - (r2r2 * r2r2) / r4d - 1);

        float fx = (circleA.x + circleB.x) / 2 + a * (circleB.x - circleA.x);
        float gx = c * (circleB.z - circleA.z) / 2;
        float ix1 = fx + gx;
        float ix2 = fx - gx;

        float fy = (circleA.z + circleB.z) / 2 + a * (circleB.z - circleA.z);
        float gy = c * (circleA.x - circleB.x) / 2;
        float iy1 = fy + gy;
        float iy2 = fy - gy;

        // if gy == 0 and gx == 0 then the circles are tangent and there is only one solution
        if (Mathf.Abs(gx) < float.Epsilon && Mathf.Abs(gy) < float.Epsilon)
        {
            intersections = new[] {
                    new Vector3(ix1, 0f, iy1)
                };
            return 1;
        }

        intersections = new[] {
                new Vector3(ix1, 0f, iy1),
                new Vector3(ix2, 0f, iy2),
            };
        return 2;
    }

    public static string RatioString(float ratio, int numberOfSegments)
    {
        var str = "[";

        for (int i = 0; i < numberOfSegments; i++)
        {
            if (Mathf.Lerp(0, numberOfSegments, ratio) < i)
                str += "▯";
            else
                str += "▮";
        }

        str += "]";

        return str;
    }

    public static Vector3 GetMeanVector(Vector3[] positions)
    {
        if (positions.Length == 0)
            return Vector3.zero;

        float x = 0f;
        float y = 0f;
        float z = 0f;

        foreach (Vector3 pos in positions)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }

        return new Vector3(x / positions.Length, y / positions.Length, z / positions.Length);
    }

    public static void DrawRay(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction * 100f);
    }

    public static int CompareRayCastHit(RaycastHit x, RaycastHit y)
    {
        return x.distance.CompareTo(y.distance);
    }

    public static Vector3 GetGroundNormalAtPosition(Vector3 position, float distance = 10f)
    {
        var groundNormal = Vector3.up;

        Ray ray = new Ray(position + Vector3.up * 0.1f, -Vector3.up);
        DrawRay(ray);

        RaycastHit[] hitInfos = Physics.RaycastAll(ray, distance, LayerMaskManager.Instance.GetLayerMask(LayerMaskType.Ground));

        hitInfos.OrderByDistance();

        if (hitInfos.Length > 0)
            groundNormal = hitInfos[0].normal;

        return groundNormal;
    }

    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public static List<T> GetComponentsInRadius<T>(Vector3 position, float distance, int layerMask, bool requireLineOfSight = false) where T : Component
    {
        List<T> results = new List<T>();

        var colliders = Physics.OverlapSphere(position, distance, layerMask);
        foreach (var collider in colliders)
        {
            Vector3 delta = (collider.transform.position - position);
            if (requireLineOfSight)
            {
                RaycastHit[] hitInfos = Physics.RaycastAll(position, delta, delta.magnitude, layerMask);

                hitInfos.OrderByDistance();
                
                if (hitInfos[0].collider != collider)
                    continue;
            }

            var component = collider.GetComponentInParent<T>();
            if (component != null)
                results.Add(component);
        }

        return results;
    }

    public static T GetComponentInCone<T>(Vector3 position, Vector3 direction, float distance, float maxAngle, int layerMask, bool requireLineOfSight = false) where T : Component
    {
        T closest = null;
        var closestDistance = float.MaxValue;
        var maxDistance = closestDistance;

        var componentsInRadius = GetComponentsInRadius<T>(position, distance, layerMask, requireLineOfSight);
        foreach (var component in componentsInRadius)
        {
            var delta = component.transform.position - position;
            var angle = Vector3.Angle(direction, delta);

            if (angle > maxAngle)
                continue;

            if (delta.magnitude > maxDistance || delta.magnitude > closestDistance)
                continue;

            closest = component;
            closestDistance = delta.magnitude;
        }

        return closest;
    }

    public static Texture2D TextureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }

        return sprite.texture;
    }

    public static bool SceneIsLoaded(int buildIndex)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex == buildIndex)
                return true;
        }

        return false;
    }
    

#if UNITY_EDITOR
    
    public static void LoadAssetsAtPath<T>(string[] paths, string filter, Action<T> callback) where T : UnityEngine.Object
    {
        string[] guids = AssetDatabase.FindAssets(filter, paths);

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            T loadedAsset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            if (loadedAsset != null)
                callback(loadedAsset);
        }
    }
    
    public static void LoadSceneEditor(string path, OpenSceneMode mode)
    {
        var scene = EditorSceneManager.OpenScene(path, mode);
        SceneManager.SetActiveScene(scene);
    }
    
#endif

    public static int GetIndexOfLoadedScene(int sceneBuildIndex)
    {
        var sceneCount = 0;
#if UNITY_EDITOR
        sceneCount = EditorSceneManager.sceneCount;
#else
		sceneCount = SceneManager.sceneCount;
#endif
        for (int i = 0; i < sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex == sceneBuildIndex)
                return i;
        }

        return -1;
    }

    public static List<T> FindComponentsWithinRadius<T>(Vector3 position, float radius, Func<T, bool> filter, LayerMask layerMask)
    {
        List<T> components = new List<T>();
        
        //TODO : could I use OverlapSphereNonAlloc instead here???
        Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask);
        foreach (Collider collider in colliders)
        {
            var component = collider.GetComponentInParent<T>();
            if (filter != null && !filter(component))
                continue;

            components.AddUnique(component);
        }

        return components;
    }
}
