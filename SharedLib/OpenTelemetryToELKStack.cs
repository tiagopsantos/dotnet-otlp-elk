using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.Extensions.Options;

namespace SharedLib
{
    public static class OpenTelemetryToELKStack
    {
        public static void AddOpenTelemetryToELKStack(this WebApplicationBuilder builder, string servicename, string endpointurl)
        {

            //OTLP Test
            //https://bartwullems.blogspot.com/2021/12/elastic-apmuse-net-opentelemetry.html

            //https://dev.to/jmourtada/how-to-setup-opentelemetry-instrumentation-in-aspnet-core-23p5
            //dotnet add package --prerelease OpenTelemetry.Exporter.Console
            //dotnet add package --prerelease OpenTelemetry.Exporter.OpenTelemetryProtocol
            //dotnet add package --prerelease OpenTelemetry.Exporter.OpenTelemetryProtocol.Logs
            //dotnet add package --prerelease OpenTelemetry.Extensions.Hosting
            //dotnet add package --prerelease OpenTelemetry.Instrumentation.AspNetCore
            //dotnet add package --prerelease OpenTelemetry.Instrumentation.Http
            //dotnet add package --prerelease OpenTelemetry.Instrumentation.Runtime

            //string servicename = "my-service-name";
            //string endpointurl = "http://localhost:8200";



            builder.Services.AddOpenTelemetry()
                .ConfigureResource(builder => builder.AddService(serviceName: servicename))
                .WithTracing(builder => builder
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(configure =>
                    {
                        configure.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                        configure.Endpoint = new Uri(endpointurl);
                    })
                )
                .WithMetrics(builder => builder
                    // Configure the resource attribute `service.name` to MyServiceName
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(servicename))
                    // Add metrics from the AspNetCore instrumentation library
                    .AddRuntimeInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(configure =>
                    {
                        configure.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                        configure.Endpoint = new Uri(endpointurl);
                    })
                );


            // Clear default logging providers used by WebApplication host.
            builder.Logging.ClearProviders();

            // Configure OpenTelemetry Logging.
            builder.Logging.AddOpenTelemetry(options =>
            {
                // Export the body of the message
                options.IncludeFormattedMessage = true; 
                options.IncludeScopes = true;
                options.ParseStateValues = true;

                // add custom processor
                options.AddProcessor(new CustomLogProcessor());

                // Configure the resource attribute `service.name` to MyServiceName
                options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(servicename));
                options.AddConsoleExporter();
                options.AddOtlpExporter(configure =>
                {
                    configure.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                    configure.Endpoint = new Uri(endpointurl);
                });

            });
        }
   }
}
