namespace EnhancedLinq.Memory.Tests.Immediate;

public class ImmediateMemoryCountTests
{
    [Test]
    public async Task CountAtLeast_Span_Various()
    {
        int[] arr = [1, 2, 3, 4, 5];
        Span<int> span = arr;
        
        bool sufficient = span.CountAtLeast(3);
        
        bool z = span.CountAtLeast(0);

        Span<int> small = new[] { 1, 2 };
        bool insufficient = small.CountAtLeast(3);
        
        bool withPred = span.CountAtLeast(x => x % 2 == 0, 2); // 2 evens: 2,4

        bool withPredInsufficient = span.CountAtLeast(x => x > 10, 1);
        
        await Assert.That(z).IsTrue();

        await Assert.That(sufficient).IsTrue();


        await Assert.That(insufficient).IsFalse();

        await Assert.That(withPred).IsTrue();

        await Assert.That(withPredInsufficient).IsFalse();
    }

    [Test]
    public async Task CountAtMost_Memory_Various()
    {
        int[] arr = [1, 2, 3, 4];
        Memory<int> mem = arr.AsMemory();

        Assert.Throws<ArgumentOutOfRangeException>(() => mem.CountAtMost(-1));

        bool atMost = mem.CountAtMost(4);
        await Assert.That(atMost).IsTrue();

        bool tooSmall = mem.CountAtMost(2);
        await Assert.That(tooSmall).IsFalse();

        bool predOk = mem.CountAtMost(x => x % 2 == 0, 2); // evens are 2 and 4 => 2, so <=2 true
        await Assert.That(predOk).IsTrue();

        bool predTooMany = mem.CountAtMost(x => x > 0, 3); // there are 4 >0 => should be false
        await Assert.That(predTooMany).IsFalse();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<ArgumentNullException>(() => mem.CountAtMost(null, 1));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}