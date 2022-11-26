using Library;

namespace App;

public class Worker : BackgroundService
{
    private readonly ServiceA _service;

    public Worker(ServiceA service)
    {
        _service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        for (var i = 0; i < 5; i++)
            await _service.DoSomeThingAsync();
    }
}
