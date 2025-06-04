using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class BuilderContextMenu
{
    //[MenuItem("Builder/All")]
    private static void BuildAll()
    {
        BuildRelease();
        BuildDebugger();
        BuildProfielering();
    }

    [MenuItem("Builder/Release")]
    private static void BuildRelease()
    {
        var path = GetBuildPlayerPath("Release");

        var options = GetBuildPlayerOptions(path, BuildOptions.None);

        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);

        ShowExplorer(path);
    }

    [MenuItem("Builder/Profielering")]
    private static void BuildProfielering()
    {
        var path = GetBuildPlayerPath("Profielering");

        var options = GetBuildPlayerOptions(path, BuildOptions.Development
            | BuildOptions.EnableDeepProfilingSupport | BuildOptions.ConnectWithProfiler);

        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);

        ShowExplorer(path);
    }

    [MenuItem("Builder/Debugger")]
    private static void BuildDebugger()
    {
        var path = GetBuildPlayerPath("Debugger");

        var options = GetBuildPlayerOptions(path, BuildOptions.Development
            | BuildOptions.AllowDebugging);

        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);

        ShowExplorer(path);
    }
    private static string GetBuildPlayerPath(string namePrefix)
    {
        var buildNameDirectory = $"{namePrefix} {DateTime.Now:dd.MM.yyyy HH mm}";

        var path = GetPathToBuildDirectory();

        path = Path.Combine(path, buildNameDirectory);

        Directory.CreateDirectory(path);

        path = Path.Combine(path, PlayerSettings.productName + ".exe");

        return path;
    }

    // в BuildPlayerWindow.DefaultBuildMethods есть публичный метод GetBuildPlayerOptions
    // для получения текущих настроек сборки, проблема в том что он вызывает окно для выбора
    // папки, что мне не нужно, т.к. путь я создаю из кода. В этом же классе есть метод
    // GetBuildPlayerOptionsInternal, в который можно передать bool: вызывать окно для выбора папки?,
    // но этот метод internal, поэтому приходится вызывать его с помощью рефлексии
    static BuildPlayerOptions GetBuildPlayerOptionsWithoutAskWindow(
    BuildPlayerOptions defaultOptions = new BuildPlayerOptions())
    {
        MethodInfo method = typeof(BuildPlayerWindow.DefaultBuildMethods).GetMethod(
            "GetBuildPlayerOptionsInternal",
            BindingFlags.NonPublic | BindingFlags.Static);

        return (BuildPlayerOptions)method.Invoke(null,
            new object[] { false, defaultOptions });
    }

    private static string GetPathToBuildDirectory()
    {
        string path = Application.dataPath;
        // удалить из пути /Assets
        var lastSlash = path.LastIndexOf("/", StringComparison.Ordinal);
        path = path.Substring(0, lastSlash);

        path += "/Builds";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }

    private static void ShowExplorer(string path)
    {
        // удалить из пути \Off-Road.exe
        var lastSlash = path.LastIndexOf("\\", StringComparison.Ordinal);
        path = path.Substring(0, lastSlash);

        // заменить символы, иначе проводник не откроет папку с билдом
        path = path.Replace('/', '\\');

        //Debug.Log(path);

        System.Diagnostics.Process.Start("explorer.exe", path);
    }
    private static BuildPlayerOptions GetBuildPlayerOptions(string path, BuildOptions buildOptions)
    {
        BuildPlayerOptions options = GetBuildPlayerOptionsWithoutAskWindow();
        options.locationPathName = path;
        options.options = buildOptions;

        return options;
    }
}