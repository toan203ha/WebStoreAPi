//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//public class Startup
//{
//    public Startup(IConfiguration configuration)
//    {
//        Configuration = configuration;
//    }

//    public IConfiguration Configuration { get; }

//    public void ConfigureServices(IServiceCollection services)
//    {
//        // Thêm dịch vụ vào container DI.
//        services.AddControllers(); // Nếu bạn xây dựng API.
//        // Hoặc
//        // services.AddMvc(); // Nếu bạn sử dụng MVC.
//    }

//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }
//        else
//        {
//            app.UseExceptionHandler("/Home/Error");
//            app.UseHsts();
//        }

//        app.UseHttpsRedirection();
//        app.UseStaticFiles();

//        app.UseRouting();

//        app.UseAuthorization();

//        app.UseEndpoints(endpoints =>
//        {
//            endpoints.MapControllers(); // Nếu bạn xây dựng API.
//            // Hoặc
//            // endpoints.MapDefaultControllerRoute(); // Nếu bạn sử dụng MVC.
//        });
//    }
//}
