namespace AlastairLundy.EnhancedLinq.Memory.Internals.Helpers;

internal static class SpanExceptionExtensions
{
    extension<T>(InvalidOperationException)
    {
        internal static void ThrowIfSpanIsEmpty(Span<T> span)
        {
            if(span.IsEmpty)
                throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
        }
        
        internal static void ThrowIfSpanIsEmpty(ReadOnlySpan<T> span)
        {
            if(span.IsEmpty)
                throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
        }

        internal static void ThrowIfMemoryIsEmpty(Memory<T> memory)
        {
            if(memory.IsEmpty)
                throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptyMemory);
        }
        
        internal static void ThrowIfMemoryIsEmpty(ReadOnlyMemory<T> memory)
        {
            if(memory.IsEmpty)
                throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptyMemory);
        }
    }
}