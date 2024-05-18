using Infrastructure.Configuration;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainFactories();

builder.Services.AddApplicationInteractors();
builder.Services.AddApplicationPresenters();

var splitIoOptionsBuilder = builder.Configuration.GetSection(SplitIoOptions.SplitIo);

builder.Services.AddInfrastructureSplitIoConfig(builder.Configuration);

var splitIoOptions = splitIoOptionsBuilder.Get<SplitIoOptions>();

if (splitIoOptions != null && splitIoOptions.SdkKey != "")
{
    try
    {
        builder.Services.AddInfrastructureSplitIoRepositories(splitIoOptions);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        Console.WriteLine("Split.IO client unable to start up. Falling back on sqlite datasource.");
        builder.Services.AddInfrastructureSqliteRepositories();
    }
}
else
{
    builder.Services.AddInfrastructureSqliteRepositories();
}

builder.Services.AddWebApiPresenters();

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