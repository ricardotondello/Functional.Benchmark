# Functional.Benchmark 🚀

[![Build](https://github.com/ricardotondello/Functional.Benchmark/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/ricardotondello/Functional.Benchmark/actions/workflows/dotnet.yml)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=ricardotondello_Functional.Benchmark&metric=alert_status)](https://sonarcloud.io/dashboard?id=ricardotondello_Functional.Benchmark)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ricardotondello_Functional.Benchmark&metric=coverage)](https://sonarcloud.io/component_measures?id=ricardotondello_Functional.Benchmark&metric=coverage)
[![NuGet latest version](https://badgen.net/nuget/v/Functional.Benchmark/latest)](https://nuget.org/packages/Functional.Benchmark)
[![NuGet downloads](https://img.shields.io/nuget/dt/Functional.Benchmark)](https://www.nuget.org/packages/Functional.Benchmark)

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

## Contributing 👥

Contributions are welcome! If you find a bug or have a feature request, please open an issue on GitHub.
If you would like to contribute code, please fork the repository and submit a pull request.

## License 📄

This project is licensed under the MIT License.
See [LICENSE](https://github.com/ricardotondello/Functional.Benchmark/blob/main/LICENSE) for more information.

## Support ☕

<a href="https://www.buymeacoffee.com/ricardotondello" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>
