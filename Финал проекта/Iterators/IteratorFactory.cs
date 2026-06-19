using System;

namespace Patterns.Iterators;

public class IteratorFactory : IIteratorFactory
{
    public IUIComponentIterator CreateDepthFirstIterator(IContainerComponent root)
    {
        return new DepthFirstIterator(root);
    }

    public IUIComponentIterator CreateFilteredIterator(IContainerComponent root, Func<IUIComponent, bool> predicate)
    {
        var dfsIterator = new DepthFirstIterator(root);
        return new FilteredIterator(dfsIterator, predicate);
    }
}
