using System.IO;
using UnityEngine;

[UnityEditor.AssetImporters.ScriptedImporterAttribute(1, ".proto")]
public class ProtoImporter : UnityEditor.AssetImporters.ScriptedImporter
{
    public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
    {
        var luaTxt = File.ReadAllText(ctx.assetPath);
        var assetsText = new TextAsset(luaTxt);
        ctx.AddObjectToAsset("main obj", assetsText);
        ctx.SetMainObject(assetsText);
    }
}