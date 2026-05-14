namespace EnhancedLinq.Memory.Tests.Immediate;

public class ImmediateMemoryIndicesExcludeSplitTests
{
    [Test]
    public async Task IndicesOf_FindsAllIndices()
    {
        int[] arr = [1, 2, 3, 2, 1];
        Span<int> span = arr;

        ICollection<int> indices = span.IndicesOf(2);
        ICollection<int> predIndices = span.IndicesOf(x => x % 2 != 0);
        
        await Assert.That(indices).IsEquivalentTo(new List<int> { 1, 3 });

        await Assert.That(predIndices).IsEquivalentTo(new List<int> { 0, 2, 4 });
    }

    [Test]
    public async Task Exclude_BySpan_RemovesAllMatchingIndices()
    {
        int[] arr = [10, 20, 30, 40, 50];
        Span<int> span = arr;
        Span<int> toRemove = new[] { 20, 40 };

        Span<int> resultSpan = span.Exclude(toRemove);
        // Should exclude elements at indices of 20 and 40 so remaining [10,30,50]
        await Assert.That(resultSpan.ToArray()).IsEquivalentTo([10, 30, 50]);
    }

    [Test]
    public async Task SplitByItemCount_SplitsCorrectly()
    {
        int[] arr = Enumerable.Range(1,7).ToArray();
        Span<int> span = arr;

        IList<int[]> parts = span.SplitByItemCount(3);
        // Expect chunks: [1,2,3], [4,5,6], [7]
        await Assert.That(parts.Count).IsEqualTo(3);
        await Assert.That(parts[0]).IsEquivalentTo([1,2,3]);
        await Assert.That(parts[2]).IsEquivalentTo([7]);
    }

    [Test]
    public async Task SplitByArrayCount_SplitsIntoApproximateEvenChunks()
    {
        int[] arr = Enumerable.Range(1,5).ToArray();
        Span<int> span = arr;

        IList<int[]> parts = span.SplitByArrayCount(2);
        // 5 items split into 2 arrays -> sizes 3 and 2
        await Assert.That(parts.Sum(p => p.Length)).IsEqualTo(5);
        await Assert.That(parts.Count).IsEqualTo(2);
    }

    [Test]
    public async Task SplitBy_Separator_Works()
    {
        int[] arr = [1,0,2,0,3];
        Span<int> span = arr;

        IList<int[]> parts = span.SplitBy(0);
        await Assert.That(parts.Count).IsEqualTo(3);
        await Assert.That(parts[0]).IsEquivalentTo([1]);
        await Assert.That(parts[2]).IsEquivalentTo([3]);
    }

    [Test]
    public async Task SplitBy_Predicate_Works()
    {
        int[] arr = [1,99,2,99,3];
        Span<int> span = arr;

        IList<int[]> parts = span.SplitBy(x => x == 99);
        await Assert.That(parts.Count).IsEqualTo(3);
        await Assert.That(parts[1]).IsEquivalentTo([2]);
    }
}