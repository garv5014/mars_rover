using System.Diagnostics.Metrics;
using System.Reflection;
using Prometheus;

namespace Mars.MissionControl;

public class Counters
{
    private Counter _rateLimitCounter;
    public Counters()
    {
        _rateLimitCounter = Metrics
            .CreateCounter("rate_limit_exceeded_total", "The number of times the rate limit was exceeded.");
    }

    public void IncrementExceededRateLimitTotal()
    {
        _rateLimitCounter.Inc();
    }
}
