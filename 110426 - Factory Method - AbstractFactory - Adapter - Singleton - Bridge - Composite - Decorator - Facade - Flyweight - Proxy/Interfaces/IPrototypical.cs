namespace Patterns;

public interface IPrototypical<T> where T : class {
    T Clone();
}
