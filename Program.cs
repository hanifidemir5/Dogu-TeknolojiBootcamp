using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlogApp.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>{
    options.AccessDeniedPath = "/Users/AccessDenied";
    options.LoginPath = "/Users/Login";
    options.LogoutPath = "/Users/Logout";
});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sql_connection"));
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute(
    name: "post_details",
    pattern: "blog/{url}",
    defaults: new { controller = "Posts", action = "Details" }
);

app.MapControllerRoute(
    name: "posts_by_tag",
    pattern: "blogs/tag/{tag}",
    defaults: new { controller = "Posts", action = "Index" }
);

app.MapControllerRoute(
     name: "user_profile",
     pattern: "profile/{username}",
     defaults: new {controller = "Users", action = "Profile"}
 );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}"
);

app.Run();
