using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class Index : ScriptableObject
{
	private static Index _instance;

	public static Index Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Resources.Load<Index>("Index");

				if (_instance == null)
				{
					Debug.LogWarning("Failed to load Index asset. Make sure it is placed directly at: Resources/Index.asset");
				}
			}

			return _instance;
		}
	}

	public Font onGUIFont;
	public IconDictionary iconDictionary;
	public SceneIndex sceneIndex;
	public KeywordConfig keywordConfig;
	public AudioLibraryData audioLibrary;
	public LayerMaskManagerData layerMaskManagerData;
	public InputActionAsset inputActionAsset;
}
