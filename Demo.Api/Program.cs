using Demo.Api.Configurations;
using Demo.Api.ExceptionMiddleware;
using Demo.Data.Configurations;
using NLog;
using NLog.Web;
using Demo.TimeRecorder.Extension;
using TimeMeasurer.Extension;
using Demo.Utils.RecaptchaV3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));

//extension methods
builder.Services.AddSwagger();
builder.Services.AddDemoDb(builder.Configuration);
builder.Services.AddIdentityCore();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

builder.Services.AddTimeRecorder();
builder.Services.AddTimeMeasurerFactory();


builder.Logging.ClearProviders();
builder.Host.UseNLog();




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
    
app.UseAuthorization();

app.UseRecaptchaMiddleware();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();



