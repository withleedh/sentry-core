using sentry_core.Middlewares;
using sentry_core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSentry();
builder.Services.AddControllers();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseSentry();



var app = builder.Build();
app.UseSentryTracing();

app.UseMiddleware<SentryReporterMiddleware>();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
// app.UseHttpsRedirection();

app.Run();