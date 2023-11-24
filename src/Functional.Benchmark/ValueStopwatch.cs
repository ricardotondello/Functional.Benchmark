using System.Diagnostics;

namespace Functional.Benchmark;

public readonly struct ValueStopwatch
{
#if !NET7_0_OR_GREATER
    private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;
#endif

    private readonly long _startTimestamp;

    private bool IsActive => _startTimestamp != 0;

    private ValueStopwatch(long startTimestamp)
    {
        _startTimestamp = startTimestamp;
    }

    public static ValueStopwatch StartNew() => new(Stopwatch.GetTimestamp());

    public TimeSpan GetElapsedTime()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException(
                "An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");
        }

        var end = Stopwatch.GetTimestamp();

#if !NET7_0_OR_GREATER
        var timestampDelta = end - _startTimestamp;
        var ticks = (long)(TimestampToTicks * timestampDelta);
        return new TimeSpan(ticks);
#else
        return Stopwatch.GetElapsedTime(_startTimestamp, end);
#endif
    }
}