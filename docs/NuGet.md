# Functional.Benchmark 🚀

A simple C# class for benchmarking asynchronous and synchronous functions with a focus on functional programming.

## Overview 📊

The FunctionalBenchmark class provides methods for benchmarking asynchronous and synchronous functions using the ValueStopwatch utility.

## Usage 💡

Simply instantiate the FunctionalBenchmark class and use the provided methods for benchmarking your functions and actions.

```csharp
var benchmark = new FunctionalBenchmark();

benchmark.BenchmarkAsync(async () => await YourAsyncFunction(), stopwatch => YourSyncStopwatchAction(stopwatch));
benchmark.Benchmark(() => YourSyncFunction(), stopwatch => YourSyncStopwatchAction(stopwatch));
// ... and more

// You also can inject in your dependencies
services.AddSingleton<IFunctionalBenchmark, FunctionalBenchmark>()

public static readonly IFunctionalBenchmark _functionalBenchmark;
// Use in your class
public MyClass(IFunctionalBenchmark functionalBenchmark)
{
    _functionalBenchmark = functionalBenchmark;
}

public async Task DoSomenthingAsync()
{
    await _functionalBenchmark.BenchmarkAsync(async () => await YourAsyncFunction(), 
        stopwatch => _logger.LogInformation($"TimeElapsed: {ElapsedTime}", stopwatch.GetElapsedTime().Milleseconds));
}
```