namespace Functional.Benchmark;

public interface IFunctionalBenchmark
{
    Task<T> BenchmarkAsync<T>(Func<Task<T>> funcTask, Action<ValueStopwatch> actionStopwatch);
    Task<T> BenchmarkAsync<T>(Func<Task<T>> funcTask, Func<ValueStopwatch, Task> actionStopwatchAsync);
    Task BenchmarkAsync(Func<Task> funcTask, Action<ValueStopwatch> actionStopwatch);
    Task BenchmarkAsync(Func<Task> funcTask, Func<ValueStopwatch, Task> actionStopwatchAsync);
    void Benchmark(Action action, Action<ValueStopwatch> actionStopwatch);
    void Benchmark(Action action, out ValueStopwatch valueStopwatch);
    T Benchmark<T>(Func<T> func, Action<ValueStopwatch> actionStopwatch);
    T Benchmark<T>(Func<T> func, out ValueStopwatch valueStopwatch);
}