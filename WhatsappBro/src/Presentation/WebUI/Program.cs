using Application;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(x =>
{
    x.MapControllerRoute("Home","{controller=Member}/{action=Login}/{Id?}");
});

app.Run();
