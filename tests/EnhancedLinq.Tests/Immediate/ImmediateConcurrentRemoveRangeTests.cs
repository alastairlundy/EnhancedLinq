using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using EnhancedLinq.Immediate.Concurrent.Ranges;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateConcurrentRemoveRangeTests
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
    public async Task RemoveRange_ItemsToRemove_RemovesSpecifiedItems_FromBag()
    {
        ConcurrentBag<int> bag = new([1, 2, 3]);
        IEnumerable<int> itemsToRemove = [2];

        ConcurrentBag<int> result = bag.RemoveRange(itemsToRemove);

        int[] actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([1, 3]);
    }

    [Test]
    public async Task RemoveRange_ItemsToRemove_EnumeratesItemsToRemoveExactlyOnce_FromBag()
    {
        ConcurrentBag<int> bag = new([1, 2, 3, 4]);
        CountingEnumerable<int> itemsToRemove = new([2, 4]);

        ConcurrentBag<int> result = bag.RemoveRange(itemsToRemove);

        int actualCount = result.Count;

        await Assert.That(actualCount).IsEqualTo(2);
        await Assert.That(itemsToRemove.EnumerationCount).IsEqualTo(1);
    }

    [Test]
    public async Task RemoveRange_ItemsToRemove_RemovesSpecifiedItems_FromProducerConsumerCollection()
    {
        ConcurrentQueue<int> queue = new([1, 2, 3]);
        IEnumerable<int> itemsToRemove = [2];

        IProducerConsumerCollection<int> result = queue.RemoveRange(itemsToRemove);

        int[] actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([1, 3]);
    }

    [Test]
    public async Task RemoveRange_ItemsToRemove_EnumeratesItemsToRemoveExactlyOnce_FromProducerConsumerCollection()
    {
        ConcurrentQueue<int> queue = new([1, 2, 3, 4]);
        CountingEnumerable<int> itemsToRemove = new([2, 4]);

        IProducerConsumerCollection<int> result = queue.RemoveRange(itemsToRemove);

        int actualCount = result.Count;

        await Assert.That(actualCount).IsEqualTo(2);
        await Assert.That(itemsToRemove.EnumerationCount).IsEqualTo(1);
    }

    [Test]
    public async Task RemoveRange_IndexAndCount_MiddleOfSequence_RemovesExactlyCountItems_FromBag()
    {
        ConcurrentBag<int> bag = new([1, 2, 3, 4, 5]);

        ConcurrentBag<int> result = bag.RemoveRange(1, 2);

        await Assert.That(result.Count).IsEqualTo(3);
    }

    [Test]
    public async Task RemoveRange_IndexAndCount_AtStart_RemovesExactlyCountItems_FromBag()
    {
        ConcurrentBag<int> bag = new([1, 2, 3, 4, 5]);

        ConcurrentBag<int> result = bag.RemoveRange(0, 2);

        await Assert.That(result.Count).IsEqualTo(3);
    }

    [Test]
    public async Task RemoveRange_IndexAndCount_AtEnd_RemovesExactlyCountItems_FromBag()
    {
        ConcurrentBag<int> bag = new([1, 2, 3, 4, 5]);

        ConcurrentBag<int> result = bag.RemoveRange(3, 2);

        await Assert.That(result.Count).IsEqualTo(3);
    }

    [Test]
    public async Task RemoveRange_IndexAndCount_MiddleOfSequence_RemovesExactlyCountItems_FromProducerConsumerCollection()
    {
        ConcurrentQueue<int> queue = new([1, 2, 3, 4, 5]);

        IProducerConsumerCollection<int> result = queue.RemoveRange(1, 2);

        int[] actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([1, 4, 5]);
    }

    [Test]
    public async Task RemoveRange_IndexAndCount_AtStart_RemovesExactlyCountItems_FromProducerConsumerCollection()
    {
        ConcurrentQueue<int> queue = new([1, 2, 3, 4, 5]);

        IProducerConsumerCollection<int> result = queue.RemoveRange(0, 2);

        int[] actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([3, 4, 5]);
    }

    [Test]
    public async Task RemoveRange_IndexAndCount_AtEnd_RemovesExactlyCountItems_FromProducerConsumerCollection()
    {
        ConcurrentQueue<int> queue = new([1, 2, 3, 4, 5]);

        IProducerConsumerCollection<int> result = queue.RemoveRange(3, 2);

        int[] actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([1, 2, 3]);
    }
}
