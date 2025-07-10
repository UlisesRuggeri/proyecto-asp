using EsteroidesToDo.Data;
using EsteroidesToDo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

/*

ValidateIssuer	            Verifica que el token fue emitido por un emisor de confianza.
ValidateAudience	        Verifica que el token es para el público correcto (tu aplicación).
ValidateLifetime	        Verifica que el token no esté expirado.
ValidateIssuerSigningKey	Verifica que la firma del token es válida.
ValidIssuer	                Nombre del emisor esperado del token.
ValidAudience	            Nombre del público esperado del token.
IssuerSigningKey	        Clave secreta para firmar y validar el token (secreta del servidor).
*/


/*esto es el middleware de jwt*/
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", Options => {

    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };

});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

builder.Services.AddAuthorization();

builder.Services.AddDbContext<EsteroidesToDoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoConnection")));

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<RegisterService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();



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



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
