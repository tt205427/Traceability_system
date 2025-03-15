using Microsoft.EntityFrameworkCore;
using Traceability_system.Data;

var builder = WebApplication.CreateBuilder(args);

// 配置数据库连接
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 添加控制器和视图支持
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 配置 HTTP 错误处理
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 使用身份验证和授权中间件
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();