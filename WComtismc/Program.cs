using Microsoft.AspNetCore.Authentication.Cookies;
using WComtismc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductCatalog, ProductCatalog>();
builder.Services.AddSingleton<IOrderStore, OrderStore>();
builder.Services.AddSingleton<IUserStore, UserStore>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
    });
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
