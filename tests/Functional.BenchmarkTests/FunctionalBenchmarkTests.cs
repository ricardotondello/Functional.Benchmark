namespace Functional.BenchmarkTests;

public class FunctionalBenchmarkTests
{
    private static readonly IFunctionalBenchmark FunctionalBenchmark = new FunctionalBenchmark();

    [Fact]
    public async Task BenchmarkAsync_ShouldRunExpectedDuration()
    {
        // Arrange
        const int delayTimeMilliseconds = 1000;
        TimeSpan? actualElapsedTime = null;

        var funcTask = new Func<Task<int>>(async () =>
        {
            await Task.Delay(delayTimeMilliseconds);
            return 10;
        });

        var actionStopwatch = new Action<ValueStopwatch>(vs => { actualElapsedTime = vs.GetElapsedTime(); });

        // Act
        var result = await FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatch);

        // Assert
        Assert.Equal(10, result);
        Assert.NotNull(actualElapsedTime);
        Assert.InRange(actualElapsedTime.Value.TotalMilliseconds, delayTimeMilliseconds - 200,
            delayTimeMilliseconds + 200);
    }

    [Fact]
    public async Task BenchmarkAsync_WhenFuncIsNullShouldThrowArgumentNullException()
    {
        // Arrange
        var actionStopwatch = new Action<ValueStopwatch>(vs => { vs.GetElapsedTime(); });

        // Act
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => FunctionalBenchmark.BenchmarkAsync(null!, actionStopwatch));
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

        // Act
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatch));
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
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task BenchmarkAsync_WithNullActionFuncReturnsSameValueAsFuncTask()
    {
        // Arrange
        var funcTask = () => Task.FromResult(42);

        // Act
        // Assert

        await Assert.ThrowsAsync<ArgumentNullException>(() => FunctionalBenchmark.BenchmarkAsync(funcTask, null!));
    }

    [Fact]
    public async Task BenchmarkAsync_ShouldCompleteGivenValidInputs()
    {
        // Arrange
        var funcTask = () => Task.CompletedTask;
        Action<ValueStopwatch> actionStopwatch = _ => { };

        // Act
        // Assert
        var exceptions =
            await Record.ExceptionAsync(() => FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatch));
        Assert.Null(exceptions);
    }

    [Fact]
    public async Task BenchmarkAsync_ShouldCompleteGivenValidInputsAsync()
    {
        // Arrange
        var funcTask = () => Task.CompletedTask;
        Func<ValueStopwatch, Task> actionStopwatchAsync = _ => Task.CompletedTask;

        // Act
        // Assert
        var exceptions =
            await Record.ExceptionAsync(() => FunctionalBenchmark.BenchmarkAsync(funcTask, actionStopwatchAsync));
        Assert.Null(exceptions);
    }

    [Fact]
    public void Benchmark_ShouldCompleteGivenValidInputs()
    {
        // Arrange
        var action = () => { };
        Action<ValueStopwatch> actionStopwatch = _ => { };

        // Act
        // Assert
        var exception = Record.Exception(() => FunctionalBenchmark.Benchmark(action, actionStopwatch));
        Assert.Null(exception);
    }

    [Fact]
    public void BenchmarkWithAction_ShouldCompleteGivenValidInputs()
    {
        // Arrange
        var action = () => { };

        // Act
        // Assert
        var exception = Record.Exception(() => FunctionalBenchmark.Benchmark(action, out _));
        Assert.Null(exception);
    }

    [Fact]
    public void Benchmark_ReturnsSameValueAsFunc()
    {
        // Arrange
        var func = () => 42;
        Action<ValueStopwatch> actionStopwatch = _ => { };

        // Act
        var result = FunctionalBenchmark.Benchmark(func, actionStopwatch);

        // Assert
        Assert.Equal(42, result);
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
        // Assert
        Assert.Throws<InvalidOperationException>(() => FunctionalBenchmark.Benchmark(func, actionStopwatch));
    }

    [Fact]
    public void BenchmarkWithFunc_ReturnsSameValueAsFunc()
    {
        // Arrange
        var func = () => 42;

        // Act
        var result = FunctionalBenchmark.Benchmark(func, out _);

        // Assert
        Assert.Equal(42, result);
    }
}