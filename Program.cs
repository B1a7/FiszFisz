using FiszFisz.Middleware;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FiszFisz");
});
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.Run();
