using System.Diagnostics;

namespace Library;
public class ServiceA
{
    public async Task DoSomeThingAsync()
    {
        using var activiy = MyActivitySource.Instance.StartActivity("MyApiCall");
        activiy?.AddTag("success", "true");

        await Task.Delay(new Random().Next(500, 2000));
        activiy?.Stop();
        
        Console.WriteLine("Hello");
    }
}

public class MyActivitySource
{
    public static string Name = nameof(MyActivitySource);
    public static ActivitySource Instance = new ActivitySource(Name, "1.0.0");
}
