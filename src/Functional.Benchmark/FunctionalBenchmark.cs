using System;
using System.Threading.Tasks;

namespace Functional.Benchmark;

public sealed class FunctionalBenchmark : IFunctionalBenchmark
{
    public async Task<T> BenchmarkAsync<T>(Func<Task<T>> funcTask, Action<ValueStopwatch> actionStopwatch)
    {
        funcTask.ThrowIfNull();
        actionStopwatch.ThrowIfNull();
        
        var valueStopwatch = ValueStopwatch.StartNew();
        var result = await funcTask();
        actionStopwatch(valueStopwatch);
        return result;
    }

    public async Task<T> BenchmarkAsync<T>(Func<Task<T>> funcTask, Func<ValueStopwatch, Task> actionStopwatchAsync)
    {
        funcTask.ThrowIfNull();
        actionStopwatchAsync.ThrowIfNull();
        
        var valueStopwatch = ValueStopwatch.StartNew();
        var result = await funcTask();
        await actionStopwatchAsync(valueStopwatch);
        return result;
    }

    public async Task BenchmarkAsync(Func<Task> funcTask, Action<ValueStopwatch> actionStopwatch)
    {
        funcTask.ThrowIfNull();
        actionStopwatch.ThrowIfNull();
        
        var valueStopwatch = ValueStopwatch.StartNew();
        await funcTask();
        actionStopwatch(valueStopwatch);
    }

    public async Task BenchmarkAsync(Func<Task> funcTask, Func<ValueStopwatch, Task> actionStopwatchAsync)
    {
        funcTask.ThrowIfNull();
        actionStopwatchAsync.ThrowIfNull();
        
        var valueStopwatch = ValueStopwatch.StartNew();
        await funcTask();
        await actionStopwatchAsync(valueStopwatch);
    }

    public void Benchmark(Action action, Action<ValueStopwatch> actionStopwatch)
    {
        actionStopwatch.ThrowIfNull();

        var valueStopwatch = ValueStopwatch.StartNew();
        action();
        actionStopwatch(valueStopwatch);
    }

    public void Benchmark(Action action, out ValueStopwatch valueStopwatch)
    {
        var vs = ValueStopwatch.StartNew();
        action();
        valueStopwatch = vs;
    }

    public T Benchmark<T>(Func<T> func, Action<ValueStopwatch> actionStopwatch)
    {
        actionStopwatch.ThrowIfNull();

        var valueStopwatch = ValueStopwatch.StartNew();
        var result = func();
        actionStopwatch(valueStopwatch);
        return result;
    }

    public T Benchmark<T>(Func<T> func, out ValueStopwatch valueStopwatch)
    {
        var vs = ValueStopwatch.StartNew();
        var result = func();
        valueStopwatch = vs;
        return result;
    }
}