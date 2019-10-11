using CinemaToon.Booking.Application.Core;
using CinemaToon.Booking.Data.Context;
using CinemaToon.Bookings.Application.Core;
using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Entities.Payment;
using CinemaToon.Utilities.Abstractions.Interfaces;
using CinemaToon.Utilities.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaToon.CinemaBooking.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<BookingDbContext>
               (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient(x => Configuration.GetSection("CinemaBooking").Get<CinemaBookingSettings>());
            services.AddTransient(x => Configuration.GetSection("Payment").Get<PaymentSettings>());

            services.AddTransient<IAPIClient, ApiClient>();
            services.AddTransient<ICinemasService, CinemaService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IPaymentService, PaymentService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Booking}/{action=Get}/{id?}");
            });
            UpdateDatabase(app);
        }


        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<BookingDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
