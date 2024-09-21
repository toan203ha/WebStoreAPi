using WebStore.Services.Oder;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký dịch vụ
builder.Services.AddHttpClient<ApiService>();
builder.Services.AddHttpClient<ProService>();
builder.Services.AddHttpClient<OrderServices>();

builder.Services.AddControllersWithViews(); // Nếu bạn sử dụng MVC
builder.Logging.AddConsole();
var app = builder.Build();

// Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
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
    pattern: "{controller=Users}/{action=Index}/{id?}"); // Thay đổi thành Users nếu bạn đang dùng controller Users

app.Run();
