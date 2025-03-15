using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.ActiveDirectory;

var builder = WebApplication.CreateBuilder(args);

// ������ݿ�������
builder.Services.AddDbContext<YourDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��ӿ���������ͼ����
builder.Services.AddControllersWithViews();

// ���� AD ����֤
builder.Services.AddAuthentication(ActiveDirectoryDefaults.AuthenticationScheme)
    .AddActiveDirectory(options =>
    {
        options.Domain = "yourdomain.com"; // �滻Ϊ��� AD ������
        options.CallbackPath = "/signin-ad";
    });

var app = builder.Build();

// ���� HTTP ����ܵ�
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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