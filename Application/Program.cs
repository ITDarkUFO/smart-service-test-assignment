using Application.Models;
using Application.Services.Admin;
using Application.Services.Work;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ApplicationDbContext>();

builder.Services.AddTransient<UserListCategoryService>();
builder.Services.AddTransient<AdminTaskUserCacheAggregateService>();
builder.Services.AddTransient<TaskUserCacheAggregateResponsibilityService>();
builder.Services.AddTransient<WorkTaskUserCacheAggregateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
