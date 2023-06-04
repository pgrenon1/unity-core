using SimpleSpritePacker;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputActionIcons))]
public class GamepadIconsEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Pack"))
        {
            Pack();
        }
    }

    public void Pack()
    {
        var gamepadIcons = target as InputActionIcons;

        var sprites = new Sprite[19]
        {
            gamepadIcons.buttonSouth,
            gamepadIcons.buttonNorth,
            gamepadIcons.buttonEast,
            gamepadIcons.buttonWest,
            gamepadIcons.startButton,
            gamepadIcons.selectButton,
            gamepadIcons.leftTrigger,
            gamepadIcons.rightTrigger,
            gamepadIcons.leftShoulder,
            gamepadIcons.rightShoulder,
            gamepadIcons.dpad,
            gamepadIcons.dpadUp,
            gamepadIcons.dpadDown,
            gamepadIcons.dpadLeft,
            gamepadIcons.dpadRight,
            gamepadIcons.leftStick,
            gamepadIcons.rightStick,
            gamepadIcons.leftStickPress,
            gamepadIcons.rightStickPress
        };

        Selection.activeObject = gamepadIcons.buttonSouth;

        var buttonPath = AssetDatabase.GetAssetPath(gamepadIcons.buttonSouth);
        var buttonFolderPath = buttonPath.Substring(0, buttonPath.LastIndexOf("/"));
        var spInstancePath = buttonFolderPath + $"/SpritePacker_{gamepadIcons.gamepadName}.asset";

        SPInstance spInstance = AssetDatabase.LoadAssetAtPath<SPInstance>(spInstancePath);
        if (spInstance == null)
        {
            SimpleSpritePackerEditor.SPInstanceEditor.CreateInstance();
            var spInstancePathOld = buttonFolderPath + "/Sprite Packer.asset";
            spInstance = AssetDatabase.LoadAssetAtPath<SPInstance>(spInstancePathOld);
            AssetDatabase.RenameAsset(spInstancePathOld, $"SpritePacker_{gamepadIcons.gamepadName}.asset");
        }

        string texturePath = AssetDatabase.GetAssetPath(spInstance.texture.GetInstanceID());
        AssetDatabase.RenameAsset(texturePath, $"InputActionsIcons_{gamepadIcons.gamepadName}.png");

        AssetDatabase.SaveAssets();

        spInstance.ClearSprites();
        for (int i = 0; i < sprites.Length; i++)
        {
            var sprite = sprites[i];
            var spriteInfo = new SPSpriteInfo();

            spriteInfo.source = sprite;
            spInstance.QueueAction_AddSprite(sprite);
        }

        var builder = new SimpleSpritePackerEditor.SPAtlasBuilder(spInstance);
        builder.RebuildAtlas();

        Selection.activeObject = spInstance.texture;
        TMP_SpriteAssetMenu.CreateSpriteAsset();

        AssetDatabase.Refresh();

        var spriteAssetPath = buttonFolderPath + $"/InputActionsIcons_{gamepadIcons.gamepadName}.asset";
        var spriteAsset = AssetDatabase.LoadAssetAtPath<TMP_SpriteAsset>(spriteAssetPath);

        spriteAsset.spriteInfoList = new System.Collections.Generic.List<TMP_Sprite>();
        var methodInfo = typeof(TMP_SpriteAssetMenu).GetMethod("UpdateSpriteInfo", BindingFlags.NonPublic | BindingFlags.Static);
        if (methodInfo != null)
        {
            methodInfo.Invoke(null, new object[] { spriteAsset });
        }

        for (int i = 0; i < spriteAsset.spriteInfoList.Count; i++)
        {
            var spriteInfo = spriteAsset.spriteInfoList[i];
            spriteInfo.name = gamepadIcons.GetGenericName(spriteInfo.sprite);
            spriteAsset.spriteCharacterTable[i].name = spriteInfo.name;
        }

        gamepadIcons.TMPSpriteAsset = spriteAsset;
    }
}
