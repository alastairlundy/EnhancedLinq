/*
    EnhancedLinq 
    Copyright (c) 2025-2026 Alastair Lundy
    
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at https://mozilla.org/MPL/2.0/. 
    */

using System.Collections;

namespace EnhancedLinq.Deferred.Enumerators;

internal class SplitByPredicateEnumerator<T> : IEnumerator<IEnumerable<T>>
{
    private readonly Func<T, bool> _predicate;

    private readonly IEnumerator<T> _enumerator;

    private int _state;
    
    internal SplitByPredicateEnumerator(IEnumerable<T> source, Func<T, bool> predicate)
    {
        _predicate = predicate;
        _state = 1;
        _enumerator =  source.GetEnumerator();
        Current = [];
    }
    
    public bool MoveNext()
    {
        if (_state == 1)
        {
            try
            {
                List<T> tempList = new List<T>();

                while (_enumerator.MoveNext())
                {
                    bool split = _predicate(_enumerator.Current);

                    if (!split)
                    {
                        tempList.Add(_enumerator.Current);
                    }
                    else
                    {
                        Current = new List<T>(tempList);
                        tempList.Clear();
                        return true;
                    }
                }
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

    public IEnumerable<T> Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}