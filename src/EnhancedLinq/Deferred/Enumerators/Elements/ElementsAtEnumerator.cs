/*
    EnhancedLinq 
    Copyright (c) 2025 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;
using System.Linq;

namespace EnhancedLinq.Deferred.Enumerators;

internal class ElementsAtEnumerator<TSource> : IEnumerator<TSource>
{
    private readonly IEnumerator<TSource> _enumerator;

    private int _state;

    internal ElementsAtEnumerator(IEnumerable<TSource> source, IEnumerable<int> indices)
    {
        _state = 1;

      IEnumerable<TSource> values = indices.Select(i => source.ElementAt(i));

      _enumerator = values.GetEnumerator();
      Current = _enumerator.Current;
    }

    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                if (_enumerator.MoveNext())
                {
                    Current = _enumerator.Current;
                    return true;
                }

                return false;
            }
            catch
            {
                Dispose();
                throw;
            }
            finally
            {
                _state = -1;
            }
        }
        
        Dispose();
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public TSource Current { get; private set; }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}