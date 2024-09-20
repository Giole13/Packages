//#if UNITY_EDITOR 
//#define DEBUG
//#endif

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngineInternal;
using UnityEditor;
using System.Reflection;


/// 
/// It overrides UnityEngine.Debug to mute debug messages completely on a platform-specific basis.
/// 
/// Putting this inside of 'Plugins' foloder is ok.
/// 
/// Important:
///     Other preprocessor directives than 'UNITY_EDITOR' does not correctly work.
/// 
/// Note:
///     [Conditional] attribute indicates to compilers that a method call or attribute should be 
///     ignored unless a specified conditional compilation symbol is defined.
/// 
/// See Also: 
///     http://msdn.microsoft.com/en-us/library/system.diagnostics.conditionalattribute.aspx
/// 
/// 2012.11. @kimsama
/// 

// 디버그를 래핑한 클래스
// 성능을 위해 특정 상황에서는 사용하지 않는다.
// 2024.09.20 / HyungJun / DEBUG_MODE심볼을 사용해 막을 수 있다.
public static class Debug
{
    public static bool isDebugBuild
    {
        get { return UnityEngine.Debug.isDebugBuild; }
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
        UnityEngine.Debug.Log(message);
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message, UnityEngine.Object context)
    {
        UnityEngine.Debug.Log(message, context);
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogError(object message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogError(object message, UnityEngine.Object context)
    {
        UnityEngine.Debug.LogError(message, context);
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
        UnityEngine.Debug.LogWarning(message.ToString());
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message, UnityEngine.Object context)
    {
        UnityEngine.Debug.LogWarning(message.ToString(), context);
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
    {
        UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
    {
        UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
        if (!condition) throw new Exception();
    }
}

// 출처 : https://upbo.tistory.com/164
// 콘솔에서 더블클릭하면 열리는 로그 창의 트레이스를 한단계 낮춰주는 함수
public class DebugCodeLocation
{
    [UnityEditor.Callbacks.OnOpenAsset()]
    private static bool OnOpenDebugLog(int instance, int line)
    {
        string name = EditorUtility.InstanceIDToObject(instance).name;
        if (!name.Equals("Debug")) return false;

        // 에디터 콘솔 윈도우의 인스턴스를 찾는다.
        var assembly = Assembly.GetAssembly(typeof(EditorWindow));
        if (assembly == null) return false;

        var consoleWindowType = assembly.GetType("UnityEditor.ConsoleWindow");
        if (consoleWindowType == null) return false;

        var consoleWindowField = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
        if (consoleWindowField == null) return false;

        var consoleWindowInstance = consoleWindowField.GetValue(null);
        if (consoleWindowInstance == null) return false;

        if (consoleWindowInstance != (object)EditorWindow.focusedWindow) return false;

        // 콘솔 윈도우 인스턴스의 활성화된 텍스트를 찾는다.
        var activeTextField = consoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
        if (activeTextField == null) return false;

        string activeTextValue = activeTextField.GetValue(consoleWindowInstance).ToString();
        if (string.IsNullOrEmpty(activeTextValue)) return false;

        // 디버그 로그를 호출한 파일 경로를 찾아 편집기로 연다.
        Match match = Regex.Match(activeTextValue, @"\(at (.+)\)");
        if (match.Success) match = match.NextMatch(); // stack trace의 첫번째를 건너뛴다.

        if (match.Success)
        {
            string path = match.Groups[1].Value;
            var split = path.Split(':');
            string filePath = split[0];
            int lineNum = Convert.ToInt32(split[1]);

            string dataPath = UnityEngine.Application.dataPath.Substring(0, UnityEngine.Application.dataPath.LastIndexOf("Assets"));
            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(dataPath + filePath, lineNum);
            return true;
        }
        return false;
    }
    // 출처: https://upbo.tistory.com/164 [메모장:티스토리]
}
