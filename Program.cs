using Microsoft.EntityFrameworkCore;
using Traceability_system.Data;

var builder = WebApplication.CreateBuilder(args);

// �������ݿ�����
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��ӿ���������ͼ֧��
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ���� HTTP ������
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