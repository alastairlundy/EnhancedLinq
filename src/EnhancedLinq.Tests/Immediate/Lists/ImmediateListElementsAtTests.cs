using System.Collections.Generic;
using EnhancedLinq.Immediate.Lists;

namespace ExtendedLinq.Tests.Immediate.Lists;

public class ImmediateListElementsAtTests
{
    private readonly Faker _faker = new();

    [Test]
    public async Task ElementsAt_WithNullSource_Throws()
    {
        List<int>? source = null;
        IList<int> indices = [0, 1, 2];
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            Task.FromResult(source!.ElementsAt(indices)));
    }

    [Test]
    public async Task ElementsAt_WithNullIndices_Throws()
    {
        List<int> source = [1, 2, 3, 4, 5];
        IList<int>? indices = null;
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            Task.FromResult(source.ElementsAt(indices!)));
    }

    [Test]
    public async Task ElementsAt_EmptyIndices_ReturnsEmptyList()
    {
        List<int> source = [1, 2, 3, 4, 5];
        IList<int> indices = Array.Empty<int>();
        
        IList<int> result = source.ElementsAt(indices);
        
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task ElementsAt_SingleIndex_ReturnsSingleElement()
    {
        List<int> source = [10, 20, 30, 40, 50];
        IList<int> indices = [2];
        
        IList<int> result = source.ElementsAt(indices);
        
        await Assert.That(result).IsEquivalentTo([30]);
    }

    [Test]
    public async Task ElementsAt_MultipleIndices_ReturnsElementsInOrder()
    {
        List<int> source = [10, 20, 30, 40, 50];
        IList<int> indices = [0, 2, 4];
        
        IList<int> result = source.ElementsAt(indices);
        
        await Assert.That(result).IsEquivalentTo([10, 30, 50]);
    }

    [Test]
    public async Task ElementsAt_OutOfRangeIndices_AreIgnored()
    {
        List<int> source = [10, 20, 30, 40, 50];
        IList<int> indices = [0, 2, 10, -1, 4]; // 10 and -1 are out of range
        
        IList<int> result = source.ElementsAt(indices);
        
        await Assert.That(result).IsEquivalentTo([10, 30, 50]); // Only valid indices
    }

    [Test]
    public async Task ElementsAt_DuplicateIndices_ReturnsDuplicateElements()
    {
        List<int> source = [10, 20, 30, 40, 50];
        IList<int> indices = [1, 1, 3, 3]; // Duplicate indices
        
        IList<int> result = source.ElementsAt(indices);
        
        await Assert.That(result).IsEquivalentTo([20, 20, 40, 40]); // Duplicates preserved
    }

    [Test]
    public async Task ElementsAt_WithStringList_WorksCorrectly()
    {
        List<string> source = ["apple", "banana", "cherry", "date"];
        IList<int> indices = [0, 2];
        
        IList<string> result = source.ElementsAt(indices);
        
        await Assert.That(result).IsEquivalentTo(["apple", "cherry"]);
    }

    [Test]
    public async Task ElementsAt_WithArray_WorksCorrectly()
    {
        int[] source = [10, 20, 30, 40, 50];
        int[] indices = [0, 2, 4];
        
        int[] result = source.ElementsAt(indices);
        
        await Assert.That(result).IsEquivalentTo([10, 30, 50]);
    }

    [Test]
    public async Task ElementsAt_WithArray_OutOfRangeIndices_AreIgnored()
    {
        int[] source = [10, 20, 30, 40, 50];
        int[] indices = [0, 2, 10, -1, 4]; // 10 and -1 are out of range
        
        int[] result = source.ElementsAt(indices);
        
        await Assert.That(result).IsEquivalentTo([10, 30, 50]); // Only valid indices
    }
}