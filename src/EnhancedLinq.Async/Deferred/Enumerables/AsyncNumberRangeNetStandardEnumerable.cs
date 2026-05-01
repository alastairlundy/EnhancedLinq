using System.Linq;
using System.Threading;
using EnhancedLinq.Async.Deferred.Enumerators;

namespace EnhancedLinq.Async.Deferred;

internal class AsyncNumberRangeNetStandardEnumerable : IAsyncEnumerable<int>
{
    private readonly List<int> _source;
    
    internal AsyncNumberRangeNetStandardEnumerable(int start, int count, int incrementor)
    {
        _source = [];
        
        int current = start;
        int end = start + count;
        
        while (current != end + incrementor)
        {
            _source.Add(current);
            
            current += incrementor;
        }
    }
    
    public IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return new AsyncNetStandardNumberRangeEnumerator(_source.ToAsyncEnumerable());
    }
}