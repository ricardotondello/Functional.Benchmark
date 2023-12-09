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
        T result;
        try
        {
            result = await funcTask();
        }
        finally
        {
            actionStopwatch.Invoke(valueStopwatch);
        }
        return result;
    }

    public async Task<T> BenchmarkAsync<T>(Func<Task<T>> funcTask, Func<ValueStopwatch, Task> actionStopwatchAsync)
    {
        funcTask.ThrowIfNull();
        actionStopwatchAsync.ThrowIfNull();
        
        var valueStopwatch = ValueStopwatch.StartNew();
        T result;
        try
        {
            result = await funcTask();
        }
        finally
        {
            await actionStopwatchAsync(valueStopwatch);
        }
        return result;
    }

    public async Task BenchmarkAsync(Func<Task> funcTask, Action<ValueStopwatch> actionStopwatch)
    {
        funcTask.ThrowIfNull();
        actionStopwatch.ThrowIfNull();
        
        var valueStopwatch = ValueStopwatch.StartNew();
        try
        {
            await funcTask();
        }
        finally
        {
            actionStopwatch(valueStopwatch);
        }
    }

    public async Task BenchmarkAsync(Func<Task> funcTask, Func<ValueStopwatch, Task> actionStopwatchAsync)
    {
        funcTask.ThrowIfNull();
        actionStopwatchAsync.ThrowIfNull();
        
        var valueStopwatch = ValueStopwatch.StartNew();
        try
        {
            await funcTask();
        }
        finally
        {
            await actionStopwatchAsync(valueStopwatch);
        }
    }

    public void Benchmark(Action action, Action<ValueStopwatch> actionStopwatch)
    {
        actionStopwatch.ThrowIfNull();

        var valueStopwatch = ValueStopwatch.StartNew();
        try
        {
            action();
        }
        finally
        {
            actionStopwatch(valueStopwatch);
        }
    }

    public void Benchmark(Action action, out ValueStopwatch valueStopwatch)
    {
        var vs = ValueStopwatch.StartNew();
        try
        {
            action();
        }
        finally
        {
            valueStopwatch = vs;
        }
    }

    public T Benchmark<T>(Func<T> func, Action<ValueStopwatch> actionStopwatch)
    {
        actionStopwatch.ThrowIfNull();

        var valueStopwatch = ValueStopwatch.StartNew();
        T result;
        try
        {
            result = func();
        }
        finally
        {
            actionStopwatch(valueStopwatch);
        }
        return result;
    }

    public T Benchmark<T>(Func<T> func, out ValueStopwatch valueStopwatch)
    {
        var vs = ValueStopwatch.StartNew();
        T result;
        try
        {
            result = func();
        }
        finally
        {
            valueStopwatch = vs;
        }
        return result;
    }
}