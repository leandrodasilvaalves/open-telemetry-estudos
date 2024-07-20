using Demo.Emails.Worker.Config;
using Demo.Emails.Worker.Infra;
using Demo.OpenTelemetry.Config;

var builder = WebApplication.CreateBuilder(args);
builder.AddOpenTelemetry();

builder.Services.AddOptions<EmailOptions>(builder.Configuration);
builder.Services.AddSingleton<IEmailSenderProvider, EmailSenderProvider>();
builder.Services.AddMassTransit(builder.Configuration);

var app = builder.Build();
app.Run();

