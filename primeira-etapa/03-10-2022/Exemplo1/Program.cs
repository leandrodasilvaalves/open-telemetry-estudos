﻿using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

class Program
{
    static ActivitySource s_source = new ActivitySource("Sample.DistributedTracing");

    static async Task Main(string[] args)
    {
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MySample"))
            .AddSource("Sample.DistributedTracing")
            .AddConsoleExporter()
            .Build();

        await DoSomeWork();
        Console.WriteLine("Example work done");
    }

    static async Task DoSomeWork()
    {
        using (Activity a = s_source.StartActivity("SomeWork"))
        {
            a.SetTag("foo", 1);
            await StepOne();
            await StepTwo();
            a.SetTag("bar", "Helloooo");
            a.SetTag("baz", new int[] { 1, 2, 3 });
        }
    }

    static async Task StepOne()
    {
        using (Activity a = s_source.StartActivity("StepOne"))
        {
            await Task.Delay(500);
        }
    }

    static async Task StepTwo()
    {
        using (Activity a = s_source.StartActivity("StepTwo"))
        {
            await Task.Delay(1000);
        }
    }
}