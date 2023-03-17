using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mars.MissionControl;

public class MetricMiddleware
{
    private readonly RequestDelegate _request;
    public MetricMiddleware(RequestDelegate request)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
    }
    public async Task Invoke(HttpContext httpContext, Counters counter)
    {
        var path = httpContext.Request.Path.Value;
        if (path == "/metrics")
        {
            await _request.Invoke(httpContext);
            return;
        }

        var statusCode = httpContext.Response.StatusCode;
        if (statusCode == 429)
        {
            counter.IncrementExceededRateLimitTotal();
        }
        await _request(httpContext);
    }
}
