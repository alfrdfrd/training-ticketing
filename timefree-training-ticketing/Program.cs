using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using timefree_training_ticketing.Adapters;
using timefree_training_ticketing.GraphQL;
using timefree_training_ticketing.Models.EF.Ticketing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

// Adapters

builder.Services.AddDbContextPool<Ticketing>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ticketing-db"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddDbContextFactory<Ticketing>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ticketing_db"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<UserAdapter>();
builder.Services.AddScoped<TicketAdapter>();
builder.Services.AddScoped<OrderAdapter>();

// Compression
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;


});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ticketing System", Version = "v2024.q1" });
});

builder.Services.AddGraphQLServer()
    .RegisterDbContext<Ticketing>()
    .SetRequestOptions(_ => new HotChocolate.Execution.Options.RequestExecutorOptions { ExecutionTimeout = TimeSpan.FromMinutes(10) })
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .AddAuthorization()
    .AddInMemorySubscriptions()
    .AddType(new UuidType('D'))
    .AddType<UploadType>()
    .AddType<DateTimeType>()
    .AddType<TimeSpanType>()
    .SetPagingOptions(
        new PagingOptions()
        {
            IncludeTotalCount = true
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseResponseCompression();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("api", "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("api", "./src/api/{controller}/{action}/{id?}");
    endpoints.MapGraphQL();
});

app.UseGraphQLVoyager();
app.Run();
