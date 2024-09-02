using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using System.Diagnostics;
using ToDo.Infrastructure.Logging;



namespace ToDo.Infrastructure.Extensions
{
    public static class LogExtension
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Http("http://localhost:5009",null)
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
                Debugger.Break();
            });

            builder.Services.AddLogging(lb=>lb.AddSerilog());


            builder.Host.UseSerilog();
            return builder;

        }
       
    }
}
