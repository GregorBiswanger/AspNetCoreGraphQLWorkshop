using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyConferece.Data;
using MyConferece.Repositories;

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
