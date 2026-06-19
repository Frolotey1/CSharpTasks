using System;
using System.Collections.Generic;

namespace Patterns.Iterators;

public interface IUIComponentIterator : IEnumerator<IUIComponent>
{
    new bool MoveNext();
    new void Reset();
    new IUIComponent Current { get; }
}
