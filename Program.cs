using Application.Interfaces;
using Application.Services;
using Infrastructure.Client;
using Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var awsSettings = builder.Configuration.GetSection("AWS").Get<AwsSettings>();
builder.Services.AddSingleton(awsSettings);
builder.Services.AddSingleton<IBedrockService, BedrockService>();
builder.Services.AddScoped<ISelectionService, SelectionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Combi.Swipe API V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
