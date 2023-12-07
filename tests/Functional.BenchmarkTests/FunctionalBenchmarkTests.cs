using System.Reflection;

namespace Functional.BenchmarkTests;

public class FunctionalBenchmarkTests
{
    private static readonly IFunctionalBenchmark FunctionalBenchmark = new FunctionalBenchmark();
    
    [Fact]
    public async Task BenchmarkAsync_ShouldRunExpectedDuration()
    {
        // Arrange
        const int delayTimeMilliseconds = 1000;
        var actualElapsedTime = 0.0d;
    
        var funcTask = new Func<Task<int>>(async () => 
        {
            await Task.Delay(delayTimeMilliseconds);
            return 10;
        });

        var actionStopwatch = new Action<ValueStopwatch>(vs =>
        {
            actualElapsedTime = vs.GetElapsedTime().TotalMilliseconds;
        });

        // Act
        var result = await FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatch);

        // Assert
        result.Should().Be(10);
        actualElapsedTime.Should().BeGreaterOrEqualTo(delayTimeMilliseconds);
    }
    
    [Fact]
    public async Task BenchmarkAsync_WhenFuncIsNullShouldThrowArgumentNullException()
    {
        // Arrange
        var actionStopwatch = new Action<ValueStopwatch>(vs =>
        {
            vs.GetElapsedTime();
        });

        var act = () => FunctionalBenchmark.BenchmarkAsync(null!, actionStopwatch);
        
        // Act
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
    
    [Fact]
    public async Task BenchmarkAsync_WhenActionIsNullShouldThrowArgumentNullException()
    {
        // Arrange
        const int delayTimeMilliseconds = 1000;

        var funcTask = new Func<Task<int>>(async () => 
        {
            await Task.Delay(delayTimeMilliseconds);
            return 10;
        });

        Action<ValueStopwatch> actionStopwatch = null!;

        var act = () => FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatch);
        
        // Act
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
    
    [Fact]
    public async Task BenchmarkAsync_ReturnsSameValueAsFuncTask()
    {
        // Arrange
        var funcTask = () => Task.FromResult(42);
        Func<ValueStopwatch, Task> actionStopwatchAsync = _ => Task.CompletedTask;

        // Act
        var result = await FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatchAsync);

        // Assert
        result.Should().Be(42);
    }
    
    [Fact]
    public async Task BenchmarkAsync_WithNullActionFuncReturnsSameValueAsFuncTask()
    {
        // Arrange
        var funcTask = () => Task.FromResult(42);

        var act = () => FunctionalBenchmark.BenchmarkAsync(funcTask, null!);
        // Act
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
    
    [Fact]
    public void BenchmarkAsync_ShouldCompleteGivenValidInputs()
    {
        // Arrange
        var funcTask = () => Task.CompletedTask;
        Action<ValueStopwatch> actionStopwatch = _ => {};

        // Act
        var act = () => FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatch);

        // Assert
        act.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task BenchmarkAsync_ShouldCompleteGivenValidInputsAsync()
    {
        // Arrange
        var funcTask = () => Task.CompletedTask;
        Func<ValueStopwatch, Task> actionStopwatchAsync = _ => Task.CompletedTask;

        // Act
        var act = () => FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatchAsync);

        // Assert
        await act.Should().NotThrowAsync();
    }
    
    [Fact]
    public void Benchmark_ShouldCompleteGivenValidInputs()
    {
        // Arrange
        var action = () => {};
        Action<ValueStopwatch> actionStopwatch = _ => {};

        // Act
        var act = () => FunctionalBenchmark.Benchmark(action, actionStopwatch);

        // Assert
        act.Should().NotThrow();
    }
    
    [Fact]
    public void BenchmarkWithAction_ShouldCompleteGivenValidInputs()
    {
        // Arrange
        var action = () => { };

        // Act
        var act = () => 
        {
            FunctionalBenchmark.Benchmark(action, out _);
        };

        // Assert
        act.Should().NotThrow();
    }
    
    [Fact]
    public void Benchmark_ReturnsSameValueAsFunc()
    {
        // Arrange
        var func = () => 42;
        Action<ValueStopwatch> actionStopwatch = _ => {};

        // Act
        var result = FunctionalBenchmark.Benchmark(func, actionStopwatch);

        // Assert
        result.Should().Be(42);
    }
    
    [Fact]
    public void Benchmark_WithInactiveStopWatch_ShouldThrow()
    {
        // Arrange
        var func = () => 42;
        Action<ValueStopwatch> actionStopwatch = s =>
        {
            s = new ValueStopwatch();
            s.GetElapsedTime();
        };

        // Act
        var act =() =>
        {
            FunctionalBenchmark.Benchmark(func, actionStopwatch);
        };

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void BenchmarkWithFunc_ReturnsSameValueAsFunc()
    {
        // Arrange
        var func = () => 42;

        // Act
        var result = FunctionalBenchmark.Benchmark(func, out _);

        // Assert
        result.Should().Be(42);
    }
}