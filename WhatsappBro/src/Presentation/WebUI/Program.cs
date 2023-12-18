using Application;
using Application.Hubs;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(x => x.AddDefaultPolicy(y => y.AllowCredentials().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(y => true)));
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/Member/Login";
        options.AccessDeniedPath = "/Member/UnAuthorized";
        options.Cookie = new()
        {
            Name = "IdentityCookie",
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            SecurePolicy = CookieSecurePolicy.Always
        };
    });


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(x =>
{
    x.MapHub<ChatHub>("/chat");
    x.MapControllerRoute("Home", "{controller=Member}/{action=Client}/{Id?}");
    x.MapControllers();
});

app.Run();
