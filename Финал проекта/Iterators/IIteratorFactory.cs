using System;

namespace Patterns.Iterators;

public interface IIteratorFactory
{
    IUIComponentIterator CreateDepthFirstIterator(IContainerComponent root);
    IUIComponentIterator CreateFilteredIterator(IContainerComponent root, Func<IUIComponent, bool> predicate);
}
