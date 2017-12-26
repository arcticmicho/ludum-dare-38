using UnityEngine;
using System.Collections;

using Stopwatch = System.Diagnostics.Stopwatch;

public class DebugTools
{
    private static Stopwatch _debugStopWatch = new Stopwatch();

    [System.Diagnostics.Conditional("DEBUG")]
    public static void ASSERT(bool condition, string message)
    {
        if (condition)
        {
            Debug.Break();
            throw new System.Exception("[ASSERT FAILED] [" + message + "]");
        }
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void LogToFilter(string inLog, string inFilterName)
    {
        Debug.Log(inLog + "\nCPAPI:{\"cmd\":\"Filter\" \"name\":\"" + inFilterName + "\"}");
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Watch(string inName, string inValue)
    {
        Debug.Log(inName + " : " + inValue + "\nCPAPI:{\"cmd\":\"Watch\" \"name\":\"" + inName + "\"}");
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Tik()
    {
        if (!_debugStopWatch.IsRunning)
        {
            _debugStopWatch.Reset();
            _debugStopWatch.Start();
        }
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Tok(string message)
    {
        if (_debugStopWatch.IsRunning)
        {
            _debugStopWatch.Stop();
            Debug.LogWarning("[" + message + "] > " + _debugStopWatch.Elapsed.TotalMilliseconds + "[ms]");
        }
    }

}

/// <summary>
/// This hides the actual implementation of debug so we can strip those functions
/// </summary>
public static class Debug
{
    [System.Diagnostics.Conditional("DEBUG")]
    public static void LogInfo(object message, UnityEngine.Object obj = null)
    {
        UnityEngine.Debug.Log(message, obj);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Log(object message, UnityEngine.Object obj = null)
    {
        UnityEngine.Debug.Log(message, obj);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void LogWarning(object message, UnityEngine.Object obj = null)
    {
        UnityEngine.Debug.LogWarning(message, obj);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void LogError(object message, UnityEngine.Object obj = null)
    {
        UnityEngine.Debug.LogError(message, obj);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void LogException(System.Exception e, UnityEngine.Object obj = null)
    {
        UnityEngine.Debug.LogError(e.Message, obj);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void LogFormat(string format, params object[] objects)
    {
        UnityEngine.Debug.LogFormat(format, objects);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Break()
    {
        UnityEngine.Debug.Break();
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Assert(bool condition, string text)
    {
        UnityEngine.Debug.Assert(condition, text);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Assert(bool condition, string text, UnityEngine.Object context)
    {
        UnityEngine.Debug.Assert(condition, text, context);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Assert(bool condition, UnityEngine.Object context)
    {
        UnityEngine.Debug.Assert(condition, context);
    }


}