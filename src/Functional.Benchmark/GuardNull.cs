using System;
using System.Threading.Tasks;

namespace Functional.Benchmark;

public static class GuardNull
{
    public static void ThrowIfNull(this Func<Task> funcTask)
    {
        if (funcTask == null)
        {
            throw new ArgumentNullException(nameof(funcTask));
        }
    }

    public static void ThrowIfNull(this Action<ValueStopwatch> actionStopwatch)
    {
        if (actionStopwatch == null)
        {
            throw new ArgumentNullException(nameof(actionStopwatch));
        }
    }
    
    public static void ThrowIfNull(this Func<ValueStopwatch, Task> actionStopwatchAsync)
    {
        if (actionStopwatchAsync == null)
        {
            throw new ArgumentNullException(nameof(actionStopwatchAsync));
        }
    }
}