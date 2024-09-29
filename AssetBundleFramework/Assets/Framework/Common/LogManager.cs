using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager
{
    public const bool ShowLog = true;
    public const bool ShowWarning = true;

    public const bool ShowUIInfo = true;
    public const bool ShowProcedure = true;
    public const bool ShowEvent = false;
    public static void LogInfo(string log)
    {
        if (ShowLog == false)
            return;
        Debug.Log(log);
    }

    public static void LogInfo(string info, Object param1)
    {
        if (ShowLog == false)
            return;
        string log = string.Format(info, param1);
        Debug.Log(log);
    }
    public static void LogInfo(string info,Object param1, Object param2)
    {
        if (ShowLog == false)
            return;
        string log = string.Format(info, param1,param2);
        Debug.Log(log);
    }
    public static void LogInfo(string info, Object param1, Object param2, Object param3)
    {
        if (ShowLog == false)
            return;
        string log = string.Format(info, param1, param2,param3);
        Debug.Log(log);
    }

    public static void LogWarning(string warning)
    {

    }
    public static void LogWarning(string warning, Object param1)
    {
        string log = string.Format(warning, param1);
        Debug.Log(log);
    }
    public static void LogWarning(string warning, Object param1, Object param2)
    {
        string log = string.Format(warning, param1, param2);
        Debug.Log(log);
    }
    public static void LogWarning(string warning, Object param1, Object param2, Object param3)
    {
        string log = string.Format(warning, param1, param2, param3);
        Debug.Log(log);
    }
    public static void LogError(string error)
    {
        Debug.LogError(error);
    }
    public static void LogError(string error, Object param1)
    {
        string log = string.Format(error, param1);
        Debug.Log(log);
    }
    public static void LogError(string error, object param1, object param2)
    {
        string log = string.Format(error, param1, param2);
        Debug.LogError(log);
    }
    public static void LogError(string error, Object param1, Object param2, Object param3)
    {
        string log = string.Format(error, param1, param2, param3);
        Debug.Log(log);
    }

    /// <summary>
    /// 流程上的Log用它输出,粉紫色
    /// </summary>
    /// <param name="info"></param>
    public static void LogProcedure(string info)
    {
        if (ShowProcedure == false)
            return;
        string log = string.Format("<color=#ff00ffff>=========={0}============</color>", info);
        Debug.Log(log);
    }

    public static void LogUIInfo(string info)
    {
        if (ShowUIInfo == false)
            return;
        string log = string.Format("<color=#00FF00>=========={0}============</color>", info);
        Debug.Log(log);
    }

    public static void LogEventInfo(string info)
    {
        if (ShowEvent == false)
            return;
        string log = string.Format("<color= #EAEAAE>=========={0}============</color>", info);
        Debug.Log(log);
    }
}
