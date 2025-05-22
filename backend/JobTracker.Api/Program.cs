using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var clientOrigin = builder.Configuration["ClientOrigin"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFront", policy =>
    {
        policy.WithOrigins(clientOrigin) 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSwagger();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(ApplicationAutoMapperConfig).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "JobTracker API");
});

app.UseCors("AllowFront");

app.UseAuthorization();

app.MapControllers();

app.Run();
