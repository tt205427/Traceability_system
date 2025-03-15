using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.ActiveDirectory;
using Traceability_system.Data;

var builder = WebApplication.CreateBuilder(args);

// 添加数据库上下文
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 添加控制器和视图服务
builder.Services.AddControllersWithViews();

// 配置 AD 域验证
builder.Services.AddAuthentication(ActiveDirectoryDefaults.AuthenticationScheme)
    .AddActiveDirectory(options =>
    {
        options.Domain = "PUW.com"; // 替换为你的实际 AD 域名称
        options.CallbackPath = "/signin-ad";
    });

var app = builder.Build();

// 配置 HTTP 请求管道
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