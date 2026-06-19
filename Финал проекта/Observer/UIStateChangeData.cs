using System;

namespace Patterns.Observer;

public record UIStateChangeData(string StateType, object OldValue, object NewValue, DateTime Timestamp);
