namespace Project;

public static class Assert {
    public static void IsTrue(bool condition, string msg) {
        if (!condition) throw new TestFailureException($"[Assert.IsTrue] {msg}");
    }
    public static void IsFalse(bool condition, string msg) {
        if (condition) throw new TestFailureException($"[Assert.IsFalse] {msg}");
    }
    public static void AreEqual<T>(T expected, T actual, string msg) {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
            throw new TestFailureException($"[Assert.AreEqual] {msg} | Expected: {expected}, Actual: {actual}");
    }
}
