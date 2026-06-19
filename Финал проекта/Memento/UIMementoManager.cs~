using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Memento;

public class UIMementoManager
{
    private readonly ConcurrentBag<(string Label, IMemento Snapshot, DateTime CreatedAt)> _history = new();
    private readonly int _maxCheckpoints;
    private readonly object _lock = new();

    public UIMementoManager(int maxCheckpoints = 20)
    {
        _maxCheckpoints = maxCheckpoints;
    }

    public void SaveCheckpoint(string label, IMemento snapshot)
    {
        if (string.IsNullOrWhiteSpace(label))
            throw new ArgumentException("Label cannot be empty", nameof(label));
        
        lock (_lock)
        {
            var list = _history.ToList();
            list.Add((label, snapshot, DateTime.UtcNow));
            
            while (list.Count > _maxCheckpoints)
                list.RemoveAt(0);
            
            _history.Clear();
            foreach (var item in list)
                _history.Add(item);
        }
    }

    public IMemento RestoreCheckpoint(string label)
    {
        lock (_lock)
        {
            var checkpoint = _history.FirstOrDefault(h => h.Label == label);
            if (checkpoint.Snapshot == null)
                throw new ArgumentException($"Checkpoint '{label}' not found");
            
            return checkpoint.Snapshot;
        }
    }

    public void ClearHistory()
    {
        lock (_lock)
        {
            _history.Clear();
        }
    }

    public IReadOnlyList<(string Label, DateTime CreatedAt)> GetCheckpoints()
    {
        lock (_lock)
        {
            return _history.Select(h => (h.Label, h.CreatedAt)).ToList();
        }
    }
}
