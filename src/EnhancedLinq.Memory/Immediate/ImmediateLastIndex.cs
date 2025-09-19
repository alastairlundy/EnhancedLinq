using System;

namespace AlastairLundy.EnhancedLinq.Memory.Immediate;

public static class ImmediateLastIndex
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="span"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int LastIndex<T>(this Span<T> span)
    {
        if(span.Length > 0)
            return span.Length - 1;

        return -1;
    }
}