using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;

namespace Api_InfoTransito.Configuration;

public sealed class DatabaseTimeoutExceptionHandler(ILogger<DatabaseTimeoutExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var sqlException = FindSqlException(exception);
        if (sqlException?.Number != -2)
            return false;

        logger.LogWarning(exception, "Timeout ao acessar o banco de dados durante {Method} {Path}.",
            httpContext.Request.Method, httpContext.Request.Path);

        httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        httpContext.Response.Headers.RetryAfter = "15";

        await httpContext.Response.WriteAsJsonAsync(new
        {
            success = false,
            errors = new[] { "O banco de dados está demorando para responder. Tente novamente em alguns instantes." }
        }, cancellationToken);

        return true;
    }

    private static SqlException? FindSqlException(Exception exception)
    {
        for (Exception? current = exception; current is not null; current = current.InnerException)
        {
            if (current is SqlException sqlException)
                return sqlException;
        }

        return null;
    }
}
