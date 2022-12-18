using Demo.Emails.Worker.Config;
using Demo.Emails.Worker.Infra;
using Demo.SharedModel.Config;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        services.AddOptions<EmailOptions>(host.Configuration);
        services.AddSingleton<IEmailSenderProvider, EmailSenderProvider>();
        services.AddMassTransit(host.Configuration);
    })
    .Build();

await host.RunAsync();
