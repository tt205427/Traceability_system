using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.ActiveDirectory;
using Traceability_system.Data;

var builder = WebApplication.CreateBuilder(args);

// ������ݿ�������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��ӿ���������ͼ����
builder.Services.AddControllersWithViews();

// ���� AD ����֤
builder.Services.AddAuthentication(ActiveDirectoryDefaults.AuthenticationScheme)
    .AddActiveDirectory(options =>
    {
        options.Domain = "PUW.com"; // �滻Ϊ���ʵ�� AD ������
        options.CallbackPath = "/signin-ad";
    });

var app = builder.Build();

// ���� HTTP ����ܵ�
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ʹ�������֤����Ȩ�м��
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();