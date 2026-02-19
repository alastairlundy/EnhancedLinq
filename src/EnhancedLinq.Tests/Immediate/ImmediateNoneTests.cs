/*
    EnhancedLinq
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
*/

using System.Collections.Generic;
using System.Linq;

namespace ExtendedLinq.Tests.Immediate;

public class ImmediateNoneTests
{
    private readonly Faker _faker = new();
    
    [Test]
    public async Task None_Empty_Enumerable_Throws()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => Task.FromResult(Enumerable.Empty<string>()
            .None(s => s.Length > 0)));
    }

    [Test]
    public async Task None_Enumerable_NoMatches_ReturnsTrue()
    {
        IList<string> source = _faker.Make(Random.Shared.Next(1, 10), _ => _faker.Lorem.Word());

        bool expected = source.None(s => s.Length == 0);
        
        await Assert.That(expected)
            .IsTrue();
    }

    [Test]
    public async Task None_Enumerable_Matches_ReturnsFalse()
    {
        IEnumerable<DateTime> source = _faker.MakeLazy(Random.Shared.Next(1, 10), _ => _faker.Date.Between(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1000)));

        bool expected = source.None(d => d.Date > DateTime.Today.Date);
        
        await Assert.That(expected)
            .IsFalse();
    }
}