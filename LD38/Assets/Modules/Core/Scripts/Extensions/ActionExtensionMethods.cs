using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionExtensionMethods
{
    public static void SafeInvoke(this Action action)
    {
        if (action != null) action();
    }

    public static void SafeInvoke<T>(this Action<T> action, T var1)
    {
        if (action != null) action(var1);
    }

    public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 var1, T2 var2)
    {
        if (action != null) action(var1, var2);
    }

    public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 var1, T2 var2, T3 var3)
    {
        if (action != null) action(var1, var2, var3);
    }

    public static void SafeInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 var1, T2 var2, T3 var3, T4 var4)
    {
        if (action != null) action(var1, var2, var3, var4);
    }
}
