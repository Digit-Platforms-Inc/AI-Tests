using API.Contracts;
using API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<OpenAiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/ai/chat", async ([FromBody] ChatRequest request, OpenAiService openAiService) =>
    {
        var response = await openAiService.GenerateResponse(request.Prompt);

        return response;
    })
    .WithName("Chat")
    .WithOpenApi();

await app.RunAsync();