/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlastairLundy.EnhancedLinq.Async.Immediate;

public static partial class EnhancedLinqAsyncImmediate
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static async Task<bool> IsEmptyAsync<TSource>(this IAsyncEnumerable<TSource> source)
    {
        bool anyAsync = await source.AnyAsync();
        
        return anyAsync == false;
    }
}