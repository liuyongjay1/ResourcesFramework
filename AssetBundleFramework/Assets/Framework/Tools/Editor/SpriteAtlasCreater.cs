using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.U2D;
using UnityEngine.U2D;
using System.IO;

public static class SpriteAtlasCreater
{
    const string cAtlasPath = "Assets/Works/Res/UIAtlas"; //图集所在目录

    const string cScrFoldContainsPath = "Assets/WorksArt/AtlasTexture"; //图片源目录路径包含的字符

    static string SelectFolderPath = "";


    [MenuItem("Assets/自动创建目标UI文件夹图集")]
    public static void SpriteAtlasCreate()
    {
        SelectFolderPath = GetCurrentAssetDirectory();
        if(!SelectFolderPath.Contains(cScrFoldContainsPath))
        {
            EditorUtility.DisplayDialog("错误","非UI图集目录无法操作！！！","确定");
            return;
        }

        var path = AutoCreateAtlas();
        EditorUtility.DisplayDialog("成功", "自动创建图集完成,路径为:"+ path, "确定");
        return;
    }

    public static string GetCurrentAssetDirectory()
    {
        foreach (var obj in Selection.GetFiltered<Object>(SelectionMode.Assets))
        {
            var path = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(path))
                continue;

            if (System.IO.Directory.Exists(path))
                return path;
            else if (System.IO.File.Exists(path))
                return System.IO.Path.GetDirectoryName(path);
        }

        return "Assets";
    }

    public static string AutoCreateAtlas()
    {
        SpriteAtlas atlas = new SpriteAtlas();
        // 设置参数 可根据项目具体情况进行设置
        var packSetting = new UnityEditor.U2D.SpriteAtlasPackingSettings()
        {
            blockOffset = 1,
            enableRotation = false,
            enableTightPacking = false,
            padding = 2,
        };
        
        atlas.SetPackingSettings(packSetting);

        SpriteAtlasTextureSettings textureSetting = new SpriteAtlasTextureSettings()
        {
            readable = false,
            generateMipMaps = false,
            sRGB = true,
            filterMode = FilterMode.Bilinear,
        };
        atlas.SetTextureSettings(textureSetting);

        TextureImporterPlatformSettings platformSetting = new TextureImporterPlatformSettings()
        {
            maxTextureSize = 2048,
            format = TextureImporterFormat.Automatic,
            //crunchedCompression = false,
            //textureCompression = TextureImporterCompression.Compressed,
            //compressionQuality = 50,
        };
        atlas.SetPlatformSettings(platformSetting);

        string pathName = "";
    
        var startIndex = SelectFolderPath.IndexOf(cScrFoldContainsPath) + cScrFoldContainsPath.Length;
        var atlasName = SelectFolderPath.Substring(startIndex);
        pathName = string.Format("{0}/{1}.spriteatlas", cAtlasPath, atlasName);
        //Debug.LogErrorFormat("pathName={0},SelectFolderPath={1}", pathName, SelectFolderPath);

        AssetDatabase.CreateAsset(atlas, pathName);

        //// 1、添加文件
        //DirectoryInfo dir = new DirectoryInfo(TexturePath);
        //// 这里我使用的是png图片，已经生成Sprite精灵了
        //FileInfo[] files = dir.GetFiles("*.png");
        //foreach (FileInfo file in files)
        //{
        //    atlas.Add(new[] { AssetDatabase.LoadAssetAtPath<Sprite>($"{TexturePath}/{file.Name}") });
        //}

        // 2、添加文件夹
        Object obj = AssetDatabase.LoadAssetAtPath(SelectFolderPath, typeof(Object));
        atlas.Add(new[] { obj });

        AssetDatabase.SaveAssets();

        return pathName;
    }

}
