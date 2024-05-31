using Application.Interfaces;
using Application.Models.Contexts;
using Application.Repositories;
using Application.Services.Admin;
using Application.Services.Work;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// DBs
builder.Services.AddTransient<AdminDbContext>();
builder.Services.AddTransient<WorkDbContext>();
builder.Services.AddTransient<PaDbContext>();

// Adm
builder.Services.AddScoped<IUserDistrictRepository, UserDistrictRepository>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
builder.Services.AddScoped<IRolesPermissionExtRepository, RolesPermissionExtRepository>();
builder.Services.AddScoped<ITenantMemberRepository, TenantMemberRepository>();

// Work
builder.Services.AddScoped<ITasksOnlineAssignedRepository, TasksOnlineAssignedRepository>();
builder.Services.AddScoped<ITaskListCategoryRepository, TaskListCategoryRepository>();
builder.Services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();

// Pa
builder.Services.AddScoped<IUserWorkTypeRepository, UserWorkTypeRepository>();

// Services
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
