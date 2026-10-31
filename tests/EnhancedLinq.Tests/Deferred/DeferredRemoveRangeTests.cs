using System.Collections;
using System.Linq;
using EnhancedLinq.Deferred.Ranges;

namespace ExtendedLinq.Tests.Deferred;

public class DeferredRemoveRangeTests
{
    private sealed class CountingEnumerable<T>(IEnumerable<T> inner) : IEnumerable<T>
    {
        public int EnumerationCount { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            EnumerationCount++;
            return inner.GetEnumerator();
        }

        System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Test]
    public async Task RemoveRange_RemovesSpecifiedItems()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        IEnumerable<int> itemsToBeRemoved = [2, 4];

        IEnumerable<int> result = source.RemoveRange(itemsToBeRemoved);

        List<int> actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([1, 3, 5]);
    }

    [Test]
    public async Task RemoveRange_NoItemsToBeRemoved_ReturnsSourceItems()
    {
        IEnumerable<int> source = [1, 2, 3];

        IEnumerable<int> result = source.RemoveRange(Enumerable.Empty<int>());

        List<int> actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task RemoveRange_EnumeratesItemsToBeRemovedExactlyOnce()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        CountingEnumerable<int> itemsToBeRemoved = new([2, 4]);

        IEnumerable<int> result = source.RemoveRange(itemsToBeRemoved);

        _ = result.ToList();

        await Assert.That(itemsToBeRemoved.EnumerationCount).IsEqualTo(1);
    }
}
