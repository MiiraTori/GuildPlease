using UnityEditor;
using UnityEngine;
using System.IO;

/// <summary>
/// スクリプトファイルを命名規則に基づいてフォルダーに自動整理するツール
/// </summary>
public class ScriptOrganizer
{
    [MenuItem("Tools/Organize Scripts by Naming Rule")]
    public static void OrganizeScripts()
    {
        string root = "Assets/Scripts";
        string[] files = Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories);

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileName(filePath);
            string targetFolder = GetTargetFolder(fileName);

            if (!string.IsNullOrEmpty(targetFolder))
            {
                string relativeTargetPath = Path.Combine("Assets/Scripts", targetFolder);
                if (!Directory.Exists(relativeTargetPath))
                {
                    Directory.CreateDirectory(relativeTargetPath);
                }

                string newPath = Path.Combine(relativeTargetPath, fileName).Replace("\\", "/");

                if (filePath != newPath)
                {
                    AssetDatabase.MoveAsset(filePath, newPath);
                    Debug.Log($"Moved {fileName} → {relativeTargetPath}");
                }
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("<color=green>✅ スクリプト整理が完了しました！</color>");
    }

    private static string GetTargetFolder(string fileName)
    {
        if (fileName.Contains("System")) return "Systems";
        if (fileName.Contains("Manager")) return "Core";
        if (fileName.Contains("DataSO") || fileName.Contains("Data")) return "Data";
        if (fileName.Contains("Component") || fileName.Contains("Stats")) return "Components";
        if (fileName.Contains("UI")) return "UI";
        if (fileName.Contains("Logger") || fileName.Contains("Utility")) return "Utilities";

        return "Misc"; // その他
    }
}