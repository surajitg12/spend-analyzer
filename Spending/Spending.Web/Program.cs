using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spending.Web.Infrastructure;
using Spending.Web.Repositories;
using System.Text.Json;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SpendingDbContext>(options =>
    options.UseCosmos(
        connectionString: builder.Configuration.GetValue<string>("CosmosConnection") ?? "",
        databaseName: builder.Configuration.GetValue<string>("CosmosDatabase") ?? "")
    );
builder.Services.AddScoped<ISpendingRepository, SpendingRepository>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.PageViewLocationFormats.Add("/Pages/Partials/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.AddControllers().AddJsonOptions(options => new JsonSerializerOptions(JsonSerializerDefaults.Web));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapControllers();

app.Run();
