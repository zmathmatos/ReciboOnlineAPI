using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qsti.ReciboOnline.Domain.Commands;
using Qsti.ReciboOnline.Domain.Commands.Solicitacao.SolicitarPublicacao;

using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
//using Qsti.ReciboOnline.Api.SignalR;

namespace Qsti.ReciboOnline.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue; //not recommended value
                options.MultipartBodyLengthLimit = long.MaxValue; //not recommended value
            });

            services.ConfigureMediatR();
            services.ConfigureServices();
            services.ConfigureRepositories();
            services.ConfigureSwagger();
            services.ConfigureAuthentication();
            services.ConfigureMVC();
            services.AddHttpContextAccessor();
            



            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            // services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowCredentials();
                x.Build();
            });

            //Configura para usarmos o MVC
            app.UseMvc();

            // app.UseSignalR(routes =>
            //{
            //    routes.MapHub<ChatHub>("/hub");
            //});

            //Cria a documentação da Api de forma automatica
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Qsti.ReciboOnline - V1");
            });


        }
    }


}
