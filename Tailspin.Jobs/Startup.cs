using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;

[assembly: OwinStartup(typeof(Tailspin.Jobs.Startup))]

namespace Tailspin.Jobs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            //app.UseErrorPage();
            //app.UseWelcomePage("/");
            app.UseHangfireDashboard("/");

            GlobalConfiguration.Configuration.UseSqlServerStorage(
                "Tailspin.Jobs.Properties.Settings.DefaultConnection",
                new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(
                () => Console.WriteLine("{0} Recurring job completed successfully!", DateTime.Now.ToString()),
                Cron.Minutely);

        }
    }
}
