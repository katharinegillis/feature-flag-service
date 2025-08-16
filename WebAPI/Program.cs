using Asp.Versioning;
using Infrastructure.Configuration;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainFactories();

builder.Services.AddApplicationInteractors();
builder.Services.AddApplicationPresenters();

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

var splitOptionsBuilder = builder.Configuration.GetSection(SplitOptions.Split);

builder.Services.AddInfrastructureSplitConfig(builder.Configuration);

var splitOptions = splitOptionsBuilder.Get<SplitOptions>();

if (splitOptions != null && splitOptions.SdkKey != "")
    try
    {
        builder.Services.AddInfrastructureSplitRepository(splitOptions);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        Console.WriteLine("Split.IO client unable to start up. Falling back on sqlite datasource.");
        builder.Services.AddInfrastructureSqliteRepository();
    }
else
    builder.Services.AddInfrastructureSqliteRepository();

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