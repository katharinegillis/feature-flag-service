using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresenters();
builder.Services.AddInteractors();

var splitioSdkKey = Environment.GetEnvironmentVariable("SPLITIO_SDK_KEY") ?? "";
var splitioTreatmentKey = Environment.GetEnvironmentVariable("SPLITIO_TREATMENT_KEY") ?? "default";

if (splitioSdkKey != "")
{
    try
    {
        builder.Services.AddSplitIoRepositories(splitioSdkKey, splitioTreatmentKey);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        Console.WriteLine("Split.IO client unable to start up. Falling back on sqlite datasource.");
        builder.Services.AddSqliteRepositories();
    }
}
else
{
    builder.Services.AddSqliteRepositories();
}

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();