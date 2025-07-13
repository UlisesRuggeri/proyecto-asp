using EsteroidesToDo.Data;
using EsteroidesToDo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Usuario/Login";
        options.LogoutPath = "/Usuario/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<EsteroidesToDoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoConnection")));

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<RegisterService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
var app = builder.Build();


app.UseSession();


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<EsteroidesToDoDbContext>();
    dataContext.Database.Migrate();
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();







app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
