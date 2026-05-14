namespace EnhancedLinq.Memory.Tests.Immediate;

public class ImmediateMemoryForEachTests
{
    [Test]
    public async Task ForEach_Span_Func_Modifies()
    {
        int[] arr = [1, 2, 3];
        Span<int> span = arr;

        span.ForEach(x => x * 2); // this overload returns void, but the Func<T,T> overload assigns back
        // The API defines ForEach(Func<T,T>) to assign back. Ensure values changed.
        await Assert.That(arr).IsEquivalentTo([2, 4, 6]);
    }

    [Test]
    public async Task ForEach_Memory_Func_ReplacesMemory()
    {
        int[] arr = [3,4,5];
        Memory<int> mem = arr.AsMemory();

        mem.ForEach(x => x - 1);

        // Memory.ForEach(Func) replaces memory with result array
        await Assert.That(mem.ToArray()).IsEquivalentTo([2,3,4]);
    }

    [Test]
    public async Task ForEach_Memory_Action_DoesNotModifyElements()
    {
        int[] arr = [3,4,5];
        Memory<int> mem = arr.AsMemory();

        int sum = 0;
        Memory<int> result =  mem.ForEach(x => sum += x);
        
        await Assert.That(sum).IsEquivalentTo(12);
        await Assert.That(mem).IsEquivalentTo([3, 4, 5]);
        await Assert.That(result).IsEquivalentTo([3,7,12]);
        // IsNotEquivalentTo may be doing referential equality checks and throws
        await Assert.That(result).IsNotEqualTo(mem);
        await Assert.That(arr).IsEquivalentTo(mem.ToArray());
    }
}