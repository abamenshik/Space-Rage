﻿#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class PathResourcesGenerator
{

    private static readonly List<string> ignoreResourcesInFolder = new()
    {
        "TextMesh Pro",
        "Plugins",
        "Zenject"
    };
    private const string generatedScriptPath = "/Game/Scripts/Development/MenuItems/ResourcesPaths_Generated.cs";

    private static StringBuilder scriptBuilder;
    private static StringBuilder resourcesPathListBuilder;


    [MenuItem("Tools/Refresh Path Resources", false, 0)]
    private static void Main()
    {
        var targetPath = Application.dataPath + generatedScriptPath;

        scriptBuilder = new();
        resourcesPathListBuilder = new();

        scriptBuilder.Append($@"
// =================================================================
//
//               THIS CODE IS GENERATED AUTOMATICALLY
//                         DO NOT EDIT
//
// =================================================================

public static class PathResources {{
");

        var resourceFolderPaths = Directory.GetDirectories("Assets",
            "Resources",
            SearchOption.AllDirectories);

        foreach (var path in resourceFolderPaths)
        {
            bool genetate = true;

            foreach (var ignoreName in ignoreResourcesInFolder)
            {
                if (path.Contains(ignoreName))
                {
                    genetate = false;
                    break;
                }
            }

            if (genetate)
                RecursivelyFolder(path, true);
        }

        scriptBuilder.AppendLine();
        scriptBuilder.AppendLine($@"public static readonly System.Collections.Generic.List<string> ALL_PATHS = new() {{");
        scriptBuilder.AppendLine(resourcesPathListBuilder.ToString());
        scriptBuilder.AppendLine("};");
        scriptBuilder.AppendLine("}");


        File.WriteAllText(targetPath, scriptBuilder.ToString());

        AssetDatabase.Refresh();
    }
    private static void RecursivelyFolder(string path, bool root = false)
    {
        if (!root)
        {
            var split = path.Split('\\');
            var directoryName = split[^1];
            directoryName = ReplaceIncorrectSymbolsTo_(directoryName);
            scriptBuilder.AppendLine($@"public static class {directoryName} {{");
        }

        foreach (string dir in Directory.GetDirectories(path))
        {
            RecursivelyFolder(dir);
        }

        foreach (var filePath in Directory.GetFiles(path))
        {
            if (filePath.Contains(".meta"))
            {
                continue;
            }

            var name = GetFileName(filePath);
            var resourcePath = GetFileResourcesPath(filePath);
            var extension = GetFileExtension(filePath);

            resourcesPathListBuilder.AppendLine($"\"{resourcePath}\",");
            scriptBuilder.Append($@"/// <summary>
/// {extension}
/// </summary>
public const string {name} = ""{resourcePath}"";
");
        }

        if (!root)
            scriptBuilder.AppendLine("}");
    }
    private static string GetFileName(string path)
    {
        // пути наподобии: Assets\Resources\Prefabs\Sound.prefab

        var lastSlash = path.LastIndexOf("\\", StringComparison.Ordinal) + 1;
        var lastDot = path.LastIndexOf(".", StringComparison.Ordinal);

        var substring = path[lastSlash..lastDot];

        substring = ReplaceIncorrectSymbolsTo_(substring);

        return substring;
    }
    private static string ReplaceIncorrectSymbolsTo_(string arg)
    {
        var chars = arg.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            var ch = chars[i];

            if (!char.IsLetter(ch) && !char.IsDigit(ch))
            {
                chars[i] = '_';
            }
        }
        return new(chars);
    }
    private static string GetFileResourcesPath(string path)
    {
        path = path.Replace("\\", "/");

        // из этого: Assets\Resources\Prefabs\Sound.prefab
        // получить это: Prefabs\Sound

        int startIndex = path.IndexOf("/Resources/", StringComparison.CurrentCultureIgnoreCase);
        var lastIndex = startIndex + "Resources".Length + 2;
        var lastDot = path.LastIndexOf(".", StringComparison.Ordinal);

        // проверка правильности путей
        //Debug.Log(Resources.Load(path[lastIndex..lastDot]).name);

        return path[lastIndex..lastDot];
    }
    private static string GetFileExtension(string path)
    {
        var lastDot = path.LastIndexOf(".", StringComparison.Ordinal) + 1;

        return path[lastDot..];
    }
}
#endif