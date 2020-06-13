using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyConferece.Data;
using MyConferece.Repositories;
using MyConference.GraphQL;

namespace MyConference
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MongoDB and Repositories ######
            services.AddSingleton<MyConferenceDataContext>();
            services.AddScoped<SpeakerRepository>();
            services.AddScoped<SessionsRepository>();

            // Add GraphQL ##############################
            services.AddScoped<IDependencyResolver>(serviceProvider => new FuncDependencyResolver(
                serviceProvider.GetRequiredService));
            services.AddScoped<MyConferenceSchema>();
            services.AddGraphQL(options =>
            {
                options.ExposeExceptions = true;
            }).AddGraphTypes(ServiceLifetime.Scoped);

            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add GraphQL ##############################
            app.UseGraphQL<MyConferenceSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}
